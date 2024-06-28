#version 330
 
// shader inputs
in vec4 positionWorld;              // fragment position in World Space
in vec4 normalWorld;                // fragment normal in World Space
in vec2 uv;                         // fragment uv texture coordinates
uniform sampler2D diffuseTexture;	// texture sampler
uniform vec3 ambientLightColor;
uniform vec3 lightPosition1;
uniform vec3 lightPosition2;
uniform vec3 lightPosition3;
uniform vec3 lightPosition4;
uniform vec3 lightPositionSpotlight;
uniform vec3 lightDirectionSpotlight;
uniform float lightInnerCutoffAngleSpotlight;
uniform float lightOuterCutoffAngleSpotlight;
uniform vec3 lightColor1;
uniform vec3 lightColor2;
uniform vec3 lightColor3;
uniform vec3 lightColor4;
uniform vec3 lightColorSpotlight;
uniform float lightIntensity1;
uniform float lightIntensity2;
uniform float lightIntensity3;
uniform float lightIntensity4;
uniform float lightIntensitySpotlight;
uniform vec3 viewPosition;

// shader output
out vec4 outputColor;

// fragment shader
void main()
{
    vec3 norm = normalize(vec3(normalWorld)); 
    vec4 ambient = vec4(ambientLightColor, 1.0); 

    vec3 lightDir1 = normalize(lightPosition1 - vec3(positionWorld)); 
    vec3 lightDir2 = normalize(lightPosition2 - vec3(positionWorld)); 
    vec3 lightDir3 = normalize(lightPosition3 - vec3(positionWorld)); 
    vec3 lightDir4 = normalize(lightPosition4 - vec3(positionWorld)); 
    
    vec3 lightDirSpotlight = normalize(lightPositionSpotlight - vec3(positionWorld));

    vec4 textureColor = texture(diffuseTexture, uv); 

    vec3 viewDir = normalize(viewPosition - vec3(positionWorld)); 

    float theta = dot(lightDirSpotlight, normalize(-lightDirectionSpotlight));
    float epsilon = lightInnerCutoffAngleSpotlight - lightOuterCutoffAngleSpotlight;
    float intensity = clamp((theta - lightOuterCutoffAngleSpotlight) / epsilon, 0.0, 1.0);

    float diff = max(dot(norm, lightDirSpotlight), 0.0);
    vec4 diffuseSpotlight = diff * vec4(lightColorSpotlight, 1.0) * lightIntensitySpotlight * intensity;

    vec3 reflectDirSpotlight = reflect(-lightDirSpotlight, norm);
    float spec = pow(max(dot(viewDir, reflectDirSpotlight), 0.0), 32.0);
    vec4 specular = vec4(lightColorSpotlight, 1.0) * spec * lightIntensitySpotlight * intensity;

    outputColor = (diffuseSpotlight + specular) * textureColor;

    float diff1 = max(dot(norm, lightDir1), 0.0);
    float diff2 = max(dot(norm, lightDir2), 0.0);
    float diff3 = max(dot(norm, lightDir3), 0.0);
    float diff4 = max(dot(norm, lightDir4), 0.0);

    vec4 diffuse1 = diff1 * vec4(lightColor1, 1.0) * lightIntensity1;
    vec4 diffuse2 = diff2 * vec4(lightColor2, 1.0) * lightIntensity2;
    vec4 diffuse3 = diff3 * vec4(lightColor3, 1.0) * lightIntensity3;
    vec4 diffuse4 = diff4 * vec4(lightColor4, 1.0) * lightIntensity4;

    vec3 reflectDir1 = reflect(-lightDir1, norm);
    vec3 reflectDir2 = reflect(-lightDir2, norm);
    vec3 reflectDir3 = reflect(-lightDir3, norm);
    vec3 reflectDir4 = reflect(-lightDir4, norm);

    float spec1 = pow(max(dot(viewDir, reflectDir1), 0.0), 32.0);
    float spec2 = pow(max(dot(viewDir, reflectDir2), 0.0), 32.0);
    float spec3 = pow(max(dot(viewDir, reflectDir3), 0.0), 32.0);
    float spec4 = pow(max(dot(viewDir, reflectDir4), 0.0), 32.0);

    vec4 specular1 = vec4(lightColor1, 1.0) * spec1 * lightIntensity1;
    vec4 specular2 = vec4(lightColor2, 1.0) * spec2 * lightIntensity2;
    vec4 specular3 = vec4(lightColor3, 1.0) * spec3 * lightIntensity3;
    vec4 specular4 = vec4(lightColor4, 1.0) * spec4 * lightIntensity4;

    outputColor += (diffuse1 + specular1) * textureColor;
    outputColor += (diffuse2 + specular2) * textureColor;
    outputColor += (diffuse3 + specular3) * textureColor;
    outputColor += (diffuse4 + specular4) * textureColor;
}