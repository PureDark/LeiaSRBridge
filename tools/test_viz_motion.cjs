// Evaluate the actual animation code with a minimal SVG DOM, including extreme positions.
const fs = require('fs');
const vm = require('vm');
const assert = require('assert');
const html = fs.readFileSync('src/app/ui/index.html.in', 'utf8');
const code = html.slice(html.indexOf('  const pose ='), html.indexOf('  function renderCaption()'));
function scene() {
  const elements = new Map();
  const element = selector => {
    if (!elements.has(selector)) elements.set(selector, {style:{},attrs:{},setAttribute(k,v){this.attrs[k]=v;},removeAttribute(k){delete this.attrs[k];}});
    return elements.get(selector);
  };
  const sandbox = {Math, document:{hidden:false,addEventListener(){}},state:{face:true,page:'overview'},deviceReady:()=>true,
    $:element, requestAnimationFrame:()=>1};
  vm.runInNewContext(code + '\nglobalThis.test={pose,viz,animateViz};',sandbox);
  return {...sandbox.test,element};
}
for (const x of [-10000,-500,-100,0,100,500,10000]) {
  for (const y of [-10000,-500,-100,0,100,500,10000]) {
    for (const z of [50,600,3000]) {
      const t=scene();Object.assign(t.pose,{x,y,z,pitch:90,yaw:90,roll:179});
      for (let i=1;i<=60;++i)t.animateViz(i*1000/60);
      const {x:dx,y:dy,scale}=t.viz;
      const radius=31*scale;
      assert(120+dx-radius>=0 && 120+dx+radius<=240);
      assert(82-dy-radius>=0 && 82-dy+radius<=168);
      assert(scale>=.85 && scale<=1.15);
      assert.strictEqual(t.pose.x,x);assert.strictEqual(t.pose.y,y);assert.strictEqual(t.pose.z,z);
    }
  }
}
const a=scene();Object.assign(a.pose,{x:20,y:80});
for(let i=1;i<=60;++i)a.animateViz(i*1000/60);
assert(Math.abs(a.viz.x)<4 && Math.abs(a.viz.y)<15,'Seated pose stays near the center');
const before=a.viz.roll; a.viz.roll=179;a.pose.roll=-179;a.animateViz(1016);
assert(a.viz.roll>179 && a.viz.roll<181,'Roll interpolation takes the short path');
console.log('VizMotion PASS: 147 extreme poses remain in SVG bounds; compact seated range; raw pose unchanged; roll wrap.');
