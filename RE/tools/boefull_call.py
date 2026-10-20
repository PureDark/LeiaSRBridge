import json,urllib.request,pathlib,sys
name=sys.argv[1]; args=json.loads(sys.argv[2]); args['database']='boefull'
r=json.load(urllib.request.urlopen(urllib.request.Request('http://127.0.0.1:18475/mcp',data=json.dumps({'jsonrpc':'2.0','id':1,'method':'tools/call','params':{'name':name,'arguments':args}}).encode(),headers={'Content-Type':'application/json'}),timeout=300))
s=r.get('result',{}); d=s.get('structuredContent',{})
url=s.get('_meta',{}).get('ida_mcp',{}).get('download_url')
if url:d=json.load(urllib.request.urlopen(url))
if not d:
 for c in s.get('content',[]):
  try:d=json.loads(c.get('text',''))
  except:pass
p=pathlib.Path('RE/evidence/boefull-'+sys.argv[3]);p.with_suffix('.json').write_text(json.dumps(d or r,ensure_ascii=False,indent=2),encoding='utf-8')
if isinstance(d,dict) and 'code' in d:p.with_suffix('.txt').write_text(d.get('code') or '',encoding='utf-8')
print(json.dumps(d or r,ensure_ascii=True,indent=2)[:int(sys.argv[4]) if len(sys.argv)>4 else 10000])
