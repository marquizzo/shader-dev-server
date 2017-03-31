uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main() {
  // Coordinate
  vec2 st = gl_FragCoord.xy / u_resolution;
  // Mouse
  vec2 m = (u_mouse / u_resolution);

  vec3 color = vec3(
    cos(st.x * 10.0 + u_time),
    sin(st.y * 10.0 + u_time),
    m.x
  );
  color.xy = (color.xy + 1.0) * 0.5;

	// Output
	gl_FragColor = vec4(color, 1.0);
}