import json,urllib.request
from pathlib import Path
ROOT=Path(__file__).resolve().parents[1]/'evidence/leia-tracking'
def rpc(name,args,tag=None):
 data={'jsonrpc':'2.0','id':1,'method':'tools/call','params':{'name':name,'arguments':args}}
 with urllib.request.urlopen(urllib.request.Request('http://127.0.0.1:18475/mcp',data=json.dumps(data).encode(),headers={'Content-Type':'application/json'}),timeout=300) as f:r=json.load(f)
 result=r.get('result',{})
 if tag:(ROOT/(tag+'.json')).write_text(json.dumps(r,indent=2),encoding='utf-8')
 return result.get('structuredContent',result)
