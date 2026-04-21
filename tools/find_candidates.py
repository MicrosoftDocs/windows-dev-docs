"""
Simple TOC scanner to list candidate feature pages.
Heuristic: a node is a candidate if it has an href and either
- it has children where at least one child has further 'items' (subsections), OR
- its children are leaf pages (have href) and at least one child is a "how-to" or article (heuristic: path contains 'how-tos' or 'tutorial' or filename contains 'tutorial' or 'how' or 'guide').

This script is intentionally conservative and prints the TOC path and href for manual review.
"""
import sys
import yaml
from pathlib import Path

TOC_PATH = Path('hub/apps/toc.yml')

def load_toc(path):
    with path.open('r', encoding='utf-8') as f:
        return yaml.safe_load(f)

def walk(items, path):
    for it in items:
        name = it.get('name')
        href = it.get('href')
        children = it.get('items') or []
        current_path = path + [name]
        # if node has href and has children
        if href and children:
            # check if children are leaves (all have href and no items)
            all_children_leaves = all((('href' in c) and (not c.get('items')) ) for c in children)
            if all_children_leaves:
                # heuristic: if any child looks like a how-to or tutorial
                def looks_like_howto(h):
                    if not h: return False
                    s = str(h).lower()
                    return ('how-to' in s) or ('how-tos' in s) or ('tutorial' in s) or ('how-to' in s) or ('howtos' in s) or ('how-' in s) or ('guide' in s)
                if any(looks_like_howto(c.get('href')) or looks_like_howto(c.get('name')) for c in children):
                    yield (' > '.join(current_path), href)
            else:
                # children exist but not all leaves — descend
                yield from walk(children, current_path)
        else:
            # no href at this node, but has children: descend
            if children:
                yield from walk(children, current_path)


def main():
    toc = load_toc(TOC_PATH)
    items = toc.get('items', [])
    for path, href in walk(items, []):
        print(f"{path}\t{href}")

if __name__ == '__main__':
    main()
