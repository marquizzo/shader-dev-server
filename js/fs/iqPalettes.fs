// Created by inigo quilez - iq/2015
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.


// A simple way to create color variation in a cheap way (yes, trigonometrics ARE cheap
// in the GPU, don't try to be smart and use a triangle wave instead).

// See http://iquilezles.org/www/articles/palettes/palettes.htm for more information

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

// Normalizes a value between 0 - 1
float normFloat(float n, float minVal, float maxVal){
	return max(0.0, min(1.0, (n-minVal) / (maxVal-minVal)));
}

vec3 pal(in float time, in vec3 bright, in vec3 contrast, in vec3 oscillation, in vec3 phase){
	return bright + contrast * cos(6.28318 * (oscillation * time + phase));
}

void main()
{
	vec2 p = gl_FragCoord.xy / u_resolution.xy;
	vec2 m = -u_mouse.xy / u_resolution.xy;

	// animate
	float dir = (mod(floor(p.y * 7.0), 2.0) - 0.5) * 2.0;
	// p.x += 0.1 * u_time * dir;

	// compute colors
	// vec3                col = pal( p.x, vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(1.0,1.0,1.0),vec3(0.0,0.33,0.67) );
	// vec3                                 brightness        contrast          oscillation       phase
	vec3                col = pal( p.x+m.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,0.0,0.0),vec3(0.0,0.33,0.67));
	if( p.y>(1.0/7.0) ) col = pal( p.x+m.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.0,0.10,0.20));
	if( p.y>(2.0/7.0) ) col = pal( p.x+m.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,1.0),vec3(0.3,0.20,0.20));
	if( p.y>(3.0/7.0) ) col = pal( p.x+m.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,1.0,0.5),vec3(0.8,0.90,0.30));
	if( p.y>(4.0/7.0) ) col = pal( p.x+m.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(1.0,0.7,0.4),vec3(0.0,0.15,0.20));
	if( p.y>(5.0/7.0) ) col = pal( p.x+m.x, vec3(0.5,0.5,0.5),vec3(0.5,0.5,0.5),vec3(2.0,1.0,0.0),vec3(0.5,0.20,0.25));
	if( p.y>(6.0/7.0) ) col = pal( p.x+m.x, vec3(0.8,0.5,0.4),vec3(0.2,0.4,0.2),vec3(2.0,1.0,1.0),vec3(0.0,0.25,0.25));


	// band
	float f = fract(p.y*7.0);
	// borders
	col *= smoothstep( 0.49, 0.47, abs(f-0.5) );
	// shadowing
	// col *= 0.5 + 0.5*sqrt(4.0*f*(1.0-f));

	gl_FragColor = vec4( col, 1.0 );
}