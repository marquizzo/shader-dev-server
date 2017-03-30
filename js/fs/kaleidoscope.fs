#define PI 3.14159265

#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

// Pretty RGB kaleidoscope
// By Silexars
// http://www.pouet.net/prod.php?which=57245
void main(){
	vec3 c;
	float l,z = u_time / 20.0;
	for(int i = 0;i < 3; i++) {
		vec2 uv,p = gl_FragCoord.xy / u_resolution;
		uv = p;
		p -= 0.5;
		p.x *= u_resolution.x / u_resolution.y;
		z += 0.07;
		l = length(p);
		uv += p / l * (sin(z) + 1.0) * abs(sin(l * 9.0 - z * 2.0));
		c[i] = 0.01 / length(abs(mod(uv, 1.0) - 0.5));
	}
	gl_FragColor = vec4(c / l, 1.0);
}