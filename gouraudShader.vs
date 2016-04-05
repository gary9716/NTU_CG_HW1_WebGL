<script id="vertexShader" type="vertex">
    precision mediump float;
    attribute vec3 aVertexPosition;
    attribute vec2 aTextureCoord;
    attribute vec3 aVertexNormal;

    uniform vec3 pointLightPosition;
    uniform vec3 ambientColor;
    uniform mat4 uMMatrix;
    uniform mat4 uVMatrix;
    uniform mat4 uPMatrix;
    uniform mat3 uNormalMatrix;
    uniform sampler2D uSampler;

    varying vec4 vColor;
    
    void main(void) {
        mat4 mvMat = uVMatrix * uMMatrix;
        vec3 L = vec3(uVMatrix * vec4(pointLightPosition, 1.0)) - vec3( mvMat * vec4(aVertexPosition,1.0));
        vec3 N = uNormalMatrix * aVertexNormal;
        float AmbientIntensity = 0.3;
        gl_Position = uPMatrix * mvMat * vec4(aVertexPosition, 1.0);
        float diffuseIntensity = 1.0;
        float diffuseLambert = max(dot(normalize(N), normalize(L)), 0.0);
        vec4 fragmentColor = texture2D(uSampler, vec2(aTextureCoord.s, aTextureCoord.t));
        vColor = vec4(ambientColor * AmbientIntensity + 
                      fragmentColor.rgb * diffuseLambert * diffuseIntensity, fragmentColor.a);
    }
</script>