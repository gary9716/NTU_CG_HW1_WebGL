<script id="vertexShader" type="vertex">
    precision mediump float;
    attribute vec3 aVertexPosition;
    attribute vec2 aTextureCoord;
    attribute vec3 aVertexNormal;

    uniform vec3 pointLightPosition;
    uniform vec3 ambientColor;
    uniform mat3 uNormalMatrix;
    uniform mat4 uMMatrix;
    uniform mat4 uVMatrix;
    uniform mat4 uPMatrix;

    varying vec4 vColor;
    varying vec3 N;
    varying vec3 vPosition;
    uniform sampler2D uSampler;

    void main(void) {
        N = aVertexNormal;
        vPosition = aVertexPosition;
        gl_Position = uPMatrix * uVMatrix * uMMatrix * vec4(aVertexPosition, 1.0);
        vec4 fragmentColor;
        fragmentColor = texture2D(uSampler, vec2(aTextureCoord.s, aTextureCoord.t));
        vColor = vec4(fragmentColor.rgb, fragmentColor.a);
    }
</script>