#version 330
 
// shader inputs
in vec4 positionWorld;              // fragment position in World Space
in vec4 normalWorld;                // fragment normal in World Space
in vec2 uv;                         // fragment uv texture coordinates
uniform sampler2D diffuseTexture;	// texture sampler
uniform vec3 ambientLightColor;
uniform vec3 lightPosition;
uniform vec3 lightColor;
uniform float lightIntensity;
uniform vec3 viewPosition;

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
    vec3 norm = normalize(vec3(normalWorld));

    vec3 lightDir = normalize(lightPosition - vec3(positionWorld));

    vec4 ambient = vec4(ambientLightColor, 1.0);

    float diff = max(dot(norm, lightDir), 0.0);
    vec4 diffuse = diff * vec4(lightColor, 1.0) * lightIntensity;

    vec3 viewDir = normalize(viewPosition - vec3(positionWorld));
    vec3 reflectDir = reflect(-lightDir, norm);

    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32.0);

    vec4 specular = vec4(lightColor, 1.0) * spec * lightIntensity;

    vec4 textureColor = texture(diffuseTexture, uv);

    outputColor = (ambient + diffuse + specular) * textureColor;
}