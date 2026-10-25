// Explicit --native-preview-test only; reads the live native UI without changing device settings.
(async () => {
  const $=s=>document.querySelector(s);
  const send=(action,extra={})=>chrome.webview.postMessage({action,...extra});
  const wait=ms=>new Promise(r=>setTimeout(r,ms));
  try {
    if(document.title!=="Stereo3DBridge")throw Error("Product title not renamed");
    for(let i=0;i<40&&!$('#heroChip').textContent.includes('原生 LeiaSR');++i)await wait(100);
    if(!$('#heroChip').textContent.includes('原生 LeiaSR'))throw Error('Native mode banner missing');
    for(const id of ['#tuningHeader','#tuningCard','[data-page="games"]','[data-action="sheet-odyssey"]'])
      if(getComputedStyle($(id)).display!=='none')throw Error('Bridge control visible: '+id);
    const recommendation=()=>Array.from(document.querySelectorAll('#checks .check')).find(e=>e.querySelector('.k').textContent==='推荐视图尺寸');
    for(let i=0;i<60&&!/\d+ × \d+ px/.test(recommendation()?.textContent||'');++i)await wait(100);
    const dimensions=recommendation()?.querySelector('.v').textContent.trim();
    if(!/^\d+ × \d+ px$/.test(dimensions||''))throw Error('Native SDK dimensions missing: '+dimensions);
    let packets=0;const poses=[];
    const listener=({data})=>{if(data.type==='pose'){++packets;poses.push(data.pose)}};
    chrome.webview.addEventListener('message',listener);
    await wait(1000);
    chrome.webview.removeEventListener('message',listener);
    if(packets<5)throw Error('No live native pose UI packets');
    const face=$('#vizFace').transform.baseVal.consolidate().matrix;
    if(face.a*face.d-face.b*face.c>=0)throw Error('Preview is not mirrored');
    send('native-preview-test-capture');
    send('native-preview-test-result',{ok:true,packets,recommendedViews:dimensions,title:$('#heroTitle').textContent,
      mirrorDeterminant:face.a*face.d-face.b*face.c,firstPose:poses[0],lastPose:poses.at(-1)});
  } catch(e) {send('native-preview-test-result',{ok:false,error:String(e)});}
})();
