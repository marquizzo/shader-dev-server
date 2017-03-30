import * as THREE from "three";

// Relative path to shaders you'd like to use
var fShader = require("./fs/simplexNoise.fs");
// vertex shader should mostly remain untouched
var vShader = require("./vs/vShader.vs");

// THREE.js setup
// Ported from THREE.js example in http://thebookofshaders.com/04/
var camera, scene, renderer;
var uniforms;

init();
animate();

function init() {
    camera = new THREE.Camera();
    camera.position.z = 1;

    scene = new THREE.Scene();

    var geometry = new THREE.PlaneBufferGeometry( 2, 2 );

    uniforms = {
        u_time: { value: 1.0 },
        u_resolution: { value: new THREE.Vector2() },
        u_mouse: { value: new THREE.Vector2() }
    };

    var material = new THREE.ShaderMaterial( {
        uniforms: uniforms,
        vertexShader: vShader,
        fragmentShader: fShader
    } );

    var mesh = new THREE.Mesh( geometry, material );
    scene.add( mesh );

    renderer = new THREE.WebGLRenderer();
    // renderer.setPixelRatio( 1 );

    document.body.appendChild( renderer.domElement );

    onWindowResize(null);
    window.addEventListener( 'resize', onWindowResize, false );

    document.onmousemove = onMouseMove;
}

function onMouseMove(event){
    uniforms.u_mouse.value.x = event.pageX
    uniforms.u_mouse.value.y = event.pageY
}

function onWindowResize(event) {
    renderer.setSize( window.innerWidth, window.innerHeight );
    uniforms.u_resolution.value.x = renderer.domElement.width;
    uniforms.u_resolution.value.y = renderer.domElement.height;
}

function animate() {
    requestAnimationFrame( animate );
    render();
}

function render() {
    uniforms.u_time.value += 0.05;
    renderer.render( scene, camera );
}