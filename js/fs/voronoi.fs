#define HASHSCALE3 vec3(443.897, 441.423, 437.195)
#define PI 3.14159265
#define OCTAVES 5

// Color transform from HSB to RGB
vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x * 6.0 + vec3(0.0, 4.0, 2.0), 6.0) - 3.0) - 1.0, 0.0, 1.0);
    rgb = rgb * rgb * (3.0 - 2.0 * rgb);
    return c.z * mix(vec3(1.0), rgb, c.y);
}

// rotate position around axis
vec2 rotate(vec2 p, float a){
	return vec2(p.x * cos(a) - p.y * sin(a), p.x * sin(a) + p.y * cos(a));
}

// Hash without sine from:
// https://www.shadertoy.com/view/4djSRW
vec2 hash22(vec2 p){
	vec3 p3 = fract(vec3(p.xyx) * HASHSCALE3);
    p3 += dot(p3, p3.yzx+19.19);
    return fract((p3.xx+p3.yz)*p3.zy);
}

// Voronoi from:
// https://thebookofshaders.com/12/
float voronoi(vec2 x, float t){
	vec2 iPos = floor(x); // Cell integer position
	vec2 fPos = fract(x); // Cell fraction position
	
	float minDist1 = 2.0;
	float minDist2 = 2.0;

	// Calculate neighboring cells
	for(int y = -1; y <= 1; y ++){
		for(int x = -1; x <= 1; x ++){
			vec2 neighborCell = vec2(x, y);
			vec2 randPoint = hash22(iPos + neighborCell);
			
			// Animate point
			randPoint = 0.5 + sin(t + randPoint * 2.0 * PI) * 0.5;

			vec2 fPosToPoint = neighborCell + randPoint - fPos;
			float dist = length(fPosToPoint);	// Euclidean
			// float dist = max(abs(fPosToPoint.x), abs(fPosToPoint.y));	// Chebyshev
			// float dist = abs(fPosToPoint.x) + abs(fPosToPoint.y);		// Manhattan
			
			if(dist < minDist1){
				minDist2 = minDist1;
				minDist1 = dist;
			}
			else if(dist < minDist2){
				minDist2 = dist;
			}
		}
	}

	return minDist2 - minDist1;
}

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main(){
	vec2 p = gl_FragCoord.xy / u_resolution.xy;
	p.x *= u_resolution.x / u_resolution.y;

	vec2 m = vec2(1.0 - (u_mouse.x / u_resolution.x), u_mouse.y / u_resolution.y);
	vec2 pos = rotate(p, (m.x + m.y) * 0.1);

	float brightness = 0.0;

	for(int i = 1; i <= OCTAVES; i++){
		pos *= float(i);
		brightness += voronoi(pos - m * 4.0, u_time * 0.1 * float(i)) / float(i);
	}

	// Add contrast
	brightness *= brightness;
	
	float hue = distance(p, m) * 0.2 + 0.5;
	gl_FragColor = vec4(hsb2rgb(vec3(hue, 0.5, brightness)), 1.0);
}