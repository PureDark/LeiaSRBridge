// Runs only with --mock --ui-test. Exercises the shipped DOM and native message path.
(async () => {
  const $ = s => document.querySelector(s);
  const post = (action, extra={}) => chrome.webview.postMessage({action,...extra});
  const wait = async f => {
    const end=performance.now()+7000;
    while(performance.now()<end) { if(f())return; await new Promise(r=>setTimeout(r,50)); }
    throw new Error('Timed out: '+f);
  };
  const pause = () => new Promise(r=>setTimeout(r,350));
  const check=(value,label)=>{if(!value)throw new Error(label);};
  const capture=async name=>{await pause();post('ui-test-capture',{name});await pause();};
  try {
    if(document.title!=="LeiaSRBridge")throw Error("Product title not renamed");
    await wait(()=>$('#heroChip').textContent.includes('离线模拟'));
    check(!$('#checks').textContent.includes('推荐视图尺寸'),'Native dimensions hidden in bridge mode');
    check(!document.querySelector('.design-ctl'),'Prototype controls removed');
    check(getComputedStyle($('.hero h1')).fontSize==='38px','Exact headline size');
    check(getComputedStyle($('.window')).backgroundColor==='rgb(250, 249, 246)','Exact window color');
    check(getComputedStyle($('.page')).padding==='40px 40px 28px','Exact page padding');
    check($('.titlebar').getBoundingClientRect().height===38,'Exact title bar');
    check(getComputedStyle($('.card')).borderRadius==='14px','Exact card radius');
    await capture('overview');
    const transforms = new Set();
    let posePackets = 0;
    const countPose = ({data}) => { if(data.type === 'pose') ++posePackets; };
    chrome.webview.addEventListener('message', countPose);
    const initialX = $('#vizHead').transform.baseVal.getItem(0).matrix.e;
    post('offset', {value:300});
    const animationStart = performance.now();
    while (performance.now() - animationStart < 500) {
      await new Promise(requestAnimationFrame);
      transforms.add($('#vizHead').getAttribute('transform'));
    }
    chrome.webview.removeEventListener('message', countPose);
    check(transforms.size >= 6, 'Head animation interpolates between pose samples');
    check(posePackets >= 9, 'High-rate pose packets reach the real web view');
    const finalX = $('#vizHead').transform.baseVal.getItem(0).matrix.e;
    check(finalX > initialX && finalX - initialX < 6, 'Head movement reduced to compact scale');
    $('[data-page="diag"]').click();
    await wait(()=>$('#mX').textContent.includes('30.0'));
    // The native diagnostics retain the full 30 mm value; only the drawing is scaled.
    post('offset', {value:80});
    $('[data-page="overview"]').click();
    await wait(()=>$('#offsetValue').textContent.includes('8.0'));

    $('[data-step="5"]').click();
    await wait(()=>$('#offsetValue').textContent.includes('8.5'));
    $('#offsetValue').click();
    const field=$('.stepper input');field.value='-3.2';field.dispatchEvent(new KeyboardEvent('keydown',{key:'Enter'}));
    await wait(()=>$('#offsetValue').textContent.includes('3.2'));
    $('[data-pred="20"]').click();
    await wait(()=>$('[data-pred="20"]').getAttribute('aria-pressed')==='true');
    $('#offsetReset').click();
    $('[data-pred="40"]').click();
    await wait(()=>$('#offsetValue').textContent.includes('8.0'));
    $('[data-page="games"]').click();
    await wait(()=>document.querySelectorAll('.game-wrap').length===1);
    $('[data-edit]').click();$('.game-edit input').value='-dx12 "test & 参数"';$('[data-save]').click();
    await wait(()=>$('.args-tag')?.textContent==='-dx12 "test & 参数"');
    $('#autoSwitch').click();await wait(()=>$('#autoSwitch').getAttribute('aria-checked')==='true');
    $('#autoSwitch').click();await wait(()=>$('#autoSwitch').getAttribute('aria-checked')==='false');
    await capture('games');
    $('[data-remove]').click();await wait(()=>$('.empty'));
    $('[data-page="diag"]').click();
    await wait(()=>$('#mZ').textContent!=='—' && $('#log').textContent.length>0);
    check(!$('#diagStatus').textContent.includes('PID 18244'),'No fake client');
    await capture('diag');
    $('[data-action="clear-log"]').click();check($('#log').textContent==='','Clear visible log');
    $('[data-page="overview"]').click();$('#menuBtn').click();$('[data-action="sheet-odyssey"]').click();
    check($('#sheet').classList.contains('open'),'Odyssey drawer opens');await capture('odyssey');
    document.dispatchEvent(new KeyboardEvent('keydown',{key:'Escape'}));
    check(!$('#sheet').classList.contains('open'),'Escape dismisses drawer');
    $('#menuBtn').click();$('[data-action="sheet-integration"]').click();
    const startup=$('#startupSwitch');
    check(startup && startup.getAttribute('role')==='switch','Startup setting available');
    const startupBefore=startup.getAttribute('aria-checked');startup.click();
    await wait(()=>$('#toast').textContent.includes('离线模拟不修改'));
    check($('#startupSwitch').getAttribute('aria-checked')===startupBefore,'Mock UI never changes actual startup registration');
    document.dispatchEvent(new KeyboardEvent('keydown',{key:'Escape'}));
    $('#menuBtn').click();$('[data-action="sheet-update"]').click();
    await wait(()=>$('#updateMessage')?.textContent.includes('离线模拟不访问'));
    check(!$('#updateInstall'),'Mock update cannot download or install');
    await capture('update');
    document.dispatchEvent(new KeyboardEvent('keydown',{key:'Escape'}));
    post('ui-test-result',{ok:true,checks:'Exact design tokens; live state; numeric offset; prediction; saved arguments; auto attach; removal; diagnostics; real logs; drawers; four captures',width:innerWidth,height:innerHeight,dpr:devicePixelRatio,animation:{posePacketsIn500ms:posePackets,distinctTransformsIn500ms:transforms.size,movePx:finalX-initialX}});
  }catch(error){post('ui-test-result',{ok:false,error:String(error),stack:error.stack});}
})();
