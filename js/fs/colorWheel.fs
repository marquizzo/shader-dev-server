#define PI 3.14159265

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x * 6.0 + vec3(0.0, 4.0, 2.0), 6.0) - 3.0) - 1.0, 0.0, 1.0);
    rgb = rgb * rgb * (3.0 - 2.0 * rgb);
    return c.z * mix(vec3(1.0), rgb, c.y);
}

// Normalizes a value between 0 - 1
float normFloat(float n, float minVal, float maxVal){
    return max(0.0, min(1.0, (n-minVal) / (maxVal-minVal)));
}

// Sin-waves a value. 0 is 0, 1 is 1, 2 is 0
float normSin(float n){
  return (cos(n * PI) - 1.0) / -2.0;
}

void main() {
	// Coordinate
	vec2 st = gl_FragCoord.xy / u_resolution;
	vec2 m = (u_mouse / u_resolution - 0.5) * 0.01;
  vec2 center = vec2(normSin(u_time * 0.07), normSin((u_time * 0.11) + PI / 2.0));

  float angle = (atan(st.y + 0.1, st.x - 0.5) + PI) / (2.0 * PI) + u_time * 0.01;
  float dist = length(st.xy - center) * 2.0;
  vec3 color = hsb2rgb(vec3(angle, 1.0, dist));
  
  color = floor(color * 10.0) / 10.0;
	gl_FragColor = vec4(color,1.0);
}