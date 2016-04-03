<script id="fragmentShader" type="fragment">
    precision mediump float;

    uniform vec3 pointLightPosition;
    uniform vec3 ambientColor;
    uniform mat3 uNormalMatrix;
    uniform mat4 uMVMatrix;
    uniform mat4 uPMatrix;

    varying vec4 vColor;
    // varying vec3 L;
    varying vec3 N;
    varying vec3 vPosition;


    void main(void) {

        float AmbientIntensity = 0.3;
        mat4 mvp = uPMatrix * uMVMatrix;

        vec3 pointLightDirection = vec3(pointLightPosition.xyz - vPosition.xyz);        
        vec3 L = vec3(mvp * vec4(pointLightDirection, 1.0));
        vec3 V = -vec3(mvp * vec4(vPosition,1.0));
        vec3 l = normalize(L);
        vec3 n = normalize(uNormalMatrix * N);
        vec3 v = normalize(V);

        float DiffuseLightIntensity = 1.0;
        vec3 DiffuseMaterialColour = vColor.rgb;        
        float diffuseLambert = max(dot(l,n),0.0);

        float shininess = 128.0;
        vec3 R = reflect(l, n);
        float specular = pow( max(0.0,dot(R,v)), shininess);
        float SpecularIntensity = 0.5;
        vec3 SpecularColour = vec3(1.0, 1.0, 1.0);
    
        gl_FragColor = vec4(ambientColor * AmbientIntensity + 
                        diffuseLambert * DiffuseMaterialColour * DiffuseLightIntensity +
                        SpecularColour * specular * SpecularIntensity, vColor.a);

    }
</script>