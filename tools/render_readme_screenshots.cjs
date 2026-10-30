// Render documentation previews from the shipped UI with explicit fixture data.
// No native bridge, camera, display or network calls. Requires Node + Playwright.
const fs = require('fs');
const path = require('path');
const {chromium} = require('playwright');
const root = path.resolve(__dirname, '..');
const output = path.join(root, 'docs', 'images');
const version = fs.readFileSync(path.join(root,'CMakeLists.txt'),'utf8').match(/project\(LeiaSRBridge VERSION ([\d.]+)/)[1];
let html = fs.readFileSync(path.join(root,'src/app/ui/index.html.in'),'utf8').replaceAll('@LEIASR_UI_VERSION@',version);
// Restore only the outside window framing from the design, leaving app layout unchanged.
const frame = '<style>html,body{width:100%;height:auto}body{display:grid;place-items:center;min-height:100vh;padding:72px 100px 104px;background:var(--backdrop)}.window{width:920px;max-width:100%;height:760px;min-height:760px;border-radius:12px;box-shadow:0 1px 0 rgba(255,255,255,.6) inset,0 30px 80px -30px rgba(40,32,20,.35),0 0 0 1px rgba(40,32,20,.08)}.readme-caption{position:fixed;bottom:25px;left:0;right:0;text-align:center;color:#7C766C;font-size:12px;letter-spacing:.3px}</style>';
const bridge = '<script>window.__uiListeners=[];window.chrome={webview:{postMessage(){},addEventListener(type,fn){if(type==="message")window.__uiListeners.push(fn)}}};window.__preview=(data)=>window.__uiListeners.forEach(fn=>fn({data}));</script>';
html=html.replace('</head>',frame+bridge+'</head>').replace('</body>','<div class="readme-caption">界面演示 · Illustrative UI preview</div></body>');
const base = {
    device:'ready', displayName:'BOE 裸眼 3D 显示器', hub:'installed', hubPath:'', hubSession:false,
    hubLaunching:false,face:true,integrated:true,integrating:false,startup:true,startupOther:false,startupError:'',
    offset:80,prediction:40,lastPrediction:40,autoAttach:false,games:[],
    calibration:true,lens:true,rotationValid:true,eyeLabel:'跟踪中',clients:'—',mock:false,message:'',
    nativePreview:false,recommendedWidth:0,recommendedHeight:0,
    update:{phase:'idle',message:'点击检查 GitHub 最新正式版本。'}
};
const scenarios=[
    {name:'overview-ready',state:base,pose:{x:15,y:0,z:600,pitch:0,yaw:-7,roll:0},title:'一切就绪'},
    {name:'overview-fallback',state:{...base,face:false,rotationValid:false,eyeLabel:'默认眼位'},pose:{x:8,y:0,z:600,pitch:0,yaw:0,roll:0},title:'暂时没有看到你'},
    {name:'native-preview',state:{...base,nativePreview:true,displayName:'LeiaSR 显示器',integrated:false,lens:false,recommendedWidth:1920,recommendedHeight:1080,message:'读取原厂头部和双眼位置，以镜像显示。'},pose:{x:-18,y:3,z:650,pitch:2,yaw:12,roll:-4},title:'正在跟随你的动作'}
];
(async()=>{
    fs.mkdirSync(output,{recursive:true});
    const browser=await chromium.launch({headless:true,channel:'msedge'});
    try{
        const page=await browser.newPage({viewport:{width:1120,height:960},deviceScaleFactor:1.5});
        await page.route('**/*',route=>route.abort());
        for(const item of scenarios){
            const errors=[];
            const onError=e=>errors.push(String(e));page.on('pageerror',onError);
            await page.setContent(html);
            await page.evaluate(item=>window.__preview({type:'state',state:item.state,pose:item.pose}),item);
            await page.locator('#heroTitle').filter({hasText:item.title}).waitFor();
            await page.evaluate(()=>document.fonts.ready);
            await page.waitForTimeout(350);
            const box=await page.locator('#win').boundingBox();
            if(box.width!==920||box.x<90||box.y<60||box.y+box.height>900)throw Error('Unexpected frame: '+JSON.stringify(box));
            if(errors.length)throw Error(errors.join('\n'));
            await page.screenshot({path:path.join(output,item.name+'.png')});
            page.off('pageerror',onError);
            console.log(item.name,JSON.stringify(box));
        }
        fs.writeFileSync(path.join(output,'README.md'),
            '# README 界面预览 / UI previews\n\n这些图片由 tools/render_readme_screenshots.cjs 使用正式 UI 模板及固定演示数据渲染；不是显示器连接或游戏运行的实测记录。只恢复设计稿的外部留白、圆角与阴影，未修改程序布局。\n\nRendered from the production UI with deterministic fixture data, not captured hardware or game sessions. Only the outer framing is restored from the design.\n\n生成：安装 Playwright 后运行 node tools/render_readme_screenshots.cjs（使用本机 Edge 无头渲染，无需启动桥接）。\n',
            'utf8');
    }finally{await browser.close();}
})().catch(e=>{console.error(e);process.exitCode=1;});
