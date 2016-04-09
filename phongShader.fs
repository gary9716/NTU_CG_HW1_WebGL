<script id="fragmentShader" type="fragment">
    precision mediump float;

    uniform vec3 pointLightPosition;
    uniform vec3 ambientColor;
    uniform mat3 uNormalMatrix;
    uniform mat4 uMMatrix;
    uniform mat4 uVMatrix;
    uniform mat4 uPMatrix;

    varying vec4 vColor;
    varying vec3 N;
    varying vec3 vPosition;


    void main(void) {

        float AmbientIntensity = 0.3;
        mat4 mvMat = uVMatrix * uMMatrix;
        mat4 mvp = uPMatrix * mvMat;
        
        vec3 V = -vec3(mvp * vec4(vPosition,1.0));
        vec3 L = (uVMatrix * vec4(pointLightPosition, 1.0)).xyz - ( mvMat * vec4(vPosition,1.0)).xyz;
        vec3 l = normalize(L);
        vec3 n = normalize(uNormalMatrix * N);
        vec3 v = normalize(V);

        float DiffuseLightIntensity = 1.0;
        vec3 DiffuseMaterialColour = vColor.rgb;        
        float diffuseLambert = max(dot(l,n),0.0);

        float shininess = 50.0;
        vec3 R = reflect(l, n);
        float specular = pow( max(0.0,dot(R,v)), shininess);
        float SpecularIntensity = 0.5;
        vec3 SpecularColour = vec3(1.0, 1.0, 1.0);
    
        gl_FragColor = vec4(ambientColor * AmbientIntensity + 
                        diffuseLambert * DiffuseMaterialColour * DiffuseLightIntensity +
                        SpecularColour * specular * SpecularIntensity, vColor.a);

    }
</script>