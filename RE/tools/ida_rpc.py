"""Thin local MCP client. Saves exact responses for the analysis record."""
import argparse
import json
import urllib.request
from pathlib import Path

p = argparse.ArgumentParser()
p.add_argument('tool')
p.add_argument('arguments', nargs='?', default='{}')
p.add_argument('--port', type=int, default=18475)
p.add_argument('--out')
p.add_argument('--timeout', type=int, default=300)
p.add_argument('--quiet', action='store_true')
a = p.parse_args()
args = json.loads(Path(a.arguments[1:]).read_text(encoding='utf-8-sig') if a.arguments.startswith('@') else a.arguments)
req = {'jsonrpc': '2.0', 'id': 1, 'method': 'tools/list' if a.tool == 'list' else 'tools/call'}
if a.tool != 'list':
    req['params'] = {'name': a.tool, 'arguments': args}
request = urllib.request.Request(f'http://127.0.0.1:{a.port}/mcp', data=json.dumps(req).encode(), headers={'Content-Type':'application/json'})
with urllib.request.urlopen(request, timeout=a.timeout) as response:
    result = json.loads(response.read())
if a.out:
    path = Path(a.out)
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
if not a.quiet:
    print(json.dumps(result.get('result', {}).get('structuredContent', result), ensure_ascii=True, indent=2))
