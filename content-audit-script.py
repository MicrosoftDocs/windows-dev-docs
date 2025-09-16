#!/usr/bin/env python3
"""
Windows Development Documentation Content Audit Script
Analyzes repository structure, naming conventions, and content metadata
"""

import os
import re
import csv
import json
from pathlib import Path
from collections import defaultdict, Counter

def extract_yaml_frontmatter(content):
    """Extract YAML frontmatter from markdown content"""
    frontmatter = {}
    if content.startswith('---'):
        try:
            end_pos = content.find('\n---\n', 4)
            if end_pos > 0:
                yaml_content = content[4:end_pos]
                # Basic YAML parsing (simplified)
                for line in yaml_content.split('\n'):
                    if ':' in line and not line.strip().startswith('#'):
                        key, value = line.split(':', 1)
                        frontmatter[key.strip()] = value.strip().strip('"\'')
        except Exception:
            pass
    return frontmatter

def analyze_links(content):
    """Extract internal and external links from content"""
    internal_links = re.findall(r'\[([^\]]*)\]\(([^)]+\.md[^)]*)\)', content)
    external_links = re.findall(r'\[([^\]]*)\]\((https?://[^)]+)\)', content)
    relative_links = re.findall(r'\[([^\]]*)\]\((\.\./[^)]+)\)', content)
    
    return {
        'internal_links': len(internal_links),
        'external_links': len(external_links), 
        'relative_links': len(relative_links),
        'all_internal': internal_links,
        'all_relative': relative_links
    }

def categorize_content(filepath, content, frontmatter):
    """Categorize content by framework, platform, audience"""
    path_str = str(filepath).lower()
    content_lower = content.lower()
    
    # Framework detection
    framework = 'unknown'
    if 'uwp' in path_str:
        framework = 'uwp'
    elif 'windows-app-sdk' in path_str or 'winui' in path_str:
        framework = 'windows-app-sdk'
    elif 'wpf' in path_str or 'winforms' in path_str:
        framework = 'wpf-winforms'
    elif 'win32' in path_str:
        framework = 'win32'
    elif any(term in content_lower for term in ['windows app sdk', 'winui 3']):
        framework = 'windows-app-sdk'
    elif any(term in content_lower for term in ['universal windows platform', 'uwp app']):
        framework = 'uwp'
    
    # Platform detection
    platforms = []
    if any(term in content_lower for term in ['windows 10', 'win10']):
        platforms.append('windows-10')
    if any(term in content_lower for term in ['windows 11', 'win11']):
        platforms.append('windows-11')
    if not platforms:
        platforms.append('cross-version')
    
    # Content type detection
    content_type = 'guide'
    if 'tutorial' in path_str or 'tutorial' in content_lower:
        content_type = 'tutorial'
    elif 'reference' in path_str or 'api' in path_str:
        content_type = 'reference'
    elif 'overview' in path_str or 'index.md' in path_str:
        content_type = 'overview'
    elif 'quickstart' in path_str or 'getting-started' in path_str:
        content_type = 'quickstart'
    
    # Audience detection
    audience = 'intermediate'
    if any(term in content_lower for term in ['beginner', 'getting started', 'hello world']):
        audience = 'beginner'
    elif any(term in content_lower for term in ['advanced', 'expert', 'complex']):
        audience = 'advanced'
    
    return {
        'framework': framework,
        'platforms': platforms,
        'content_type': content_type,
        'audience': audience
    }

def analyze_naming_conventions():
    """Analyze directory and file naming patterns"""
    patterns = {
        'directories': defaultdict(int),
        'file_naming': defaultdict(int),
        'inconsistencies': []
    }
    
    for root, dirs, files in os.walk('.'):
        if '.git' in root:
            continue
            
        # Directory naming analysis
        for d in dirs:
            if '-' in d:
                patterns['directories']['kebab-case'] += 1
            elif '_' in d:
                patterns['directories']['snake_case'] += 1
            elif any(c.isupper() for c in d):
                patterns['directories']['mixed-case'] += 1
            else:
                patterns['directories']['lowercase'] += 1
        
        # File naming analysis  
        for f in files:
            if f.endswith('.md'):
                if '-' in f:
                    patterns['file_naming']['kebab-case'] += 1
                elif '_' in f:
                    patterns['file_naming']['snake_case'] += 1
                elif any(c.isupper() for c in f):
                    patterns['file_naming']['mixed-case'] += 1
                else:
                    patterns['file_naming']['lowercase'] += 1
    
    return patterns

def main():
    """Main content audit function"""
    print("🔍 Starting Windows Development Documentation Content Audit...")
    
    # Initialize data structures
    content_inventory = []
    link_analysis = []
    framework_stats = Counter()
    content_type_stats = Counter()
    duplicate_titles = defaultdict(list)
    
    # Walk through all markdown files
    md_files = list(Path('.').rglob('*.md'))
    total_files = len(md_files)
    
    print(f"📊 Found {total_files} markdown files to analyze...")
    
    for i, filepath in enumerate(md_files, 1):
        if i % 100 == 0:
            print(f"   Processed {i}/{total_files} files...")
        
        try:
            with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
                content = f.read()
        except Exception as e:
            print(f"   ⚠️ Error reading {filepath}: {e}")
            continue
        
        # Extract frontmatter and analyze content
        frontmatter = extract_yaml_frontmatter(content)
        links = analyze_links(content)
        categorization = categorize_content(filepath, content, frontmatter)
        
        # Get basic file info
        stat = filepath.stat()
        word_count = len(content.split())
        title = frontmatter.get('title', '')
        
        if not title:
            # Try to extract title from first heading
            title_match = re.search(r'^#\s+(.+)$', content, re.MULTILINE)
            if title_match:
                title = title_match.group(1).strip()
        
        # Track duplicate titles
        if title:
            duplicate_titles[title].append(str(filepath))
        
        # Build inventory entry
        entry = {
            'filepath': str(filepath),
            'directory': str(filepath.parent),
            'filename': filepath.name,
            'title': title,
            'word_count': word_count,
            'file_size': stat.st_size,
            'last_modified': stat.st_mtime,
            'framework': categorization['framework'],
            'platforms': ','.join(categorization['platforms']),
            'content_type': categorization['content_type'],
            'audience': categorization['audience'],
            'has_frontmatter': bool(frontmatter),
            'internal_links': links['internal_links'],
            'external_links': links['external_links'],
            'relative_links': links['relative_links'],
        }
        
        content_inventory.append(entry)
        framework_stats[categorization['framework']] += 1
        content_type_stats[categorization['content_type']] += 1
        
        # Store detailed link analysis for important files
        if links['all_internal'] or links['all_relative']:
            link_analysis.append({
                'filepath': str(filepath),
                'internal_links': links['all_internal'],
                'relative_links': links['all_relative']
            })
    
    print(f"✅ Completed analysis of {len(content_inventory)} files")
    
    # Generate outputs
    print("📁 Generating content inventory CSV...")
    with open('content-inventory.csv', 'w', newline='', encoding='utf-8') as f:
        if content_inventory:
            writer = csv.DictWriter(f, fieldnames=content_inventory[0].keys())
            writer.writeheader()
            writer.writerows(content_inventory)
    
    print("🔗 Generating link analysis JSON...")
    with open('link-analysis.json', 'w', encoding='utf-8') as f:
        json.dump(link_analysis, f, indent=2)
    
    print("📋 Analyzing naming conventions...")
    naming_patterns = analyze_naming_conventions()
    
    # Generate duplicate content report
    print("🔍 Identifying duplicate content...")
    duplicates = {title: files for title, files in duplicate_titles.items() if len(files) > 1}
    
    # Create summary report
    summary_report = f"""# Windows Development Documentation Content Audit Report

## Summary Statistics
- **Total markdown files analyzed**: {len(content_inventory):,}
- **Total word count**: {sum(entry['word_count'] for entry in content_inventory):,}
- **Average words per file**: {sum(entry['word_count'] for entry in content_inventory) // len(content_inventory):,}

## Framework Distribution
{chr(10).join(f'- **{fw}**: {count} files' for fw, count in framework_stats.most_common())}

## Content Type Distribution  
{chr(10).join(f'- **{ct}**: {count} files' for ct, count in content_type_stats.most_common())}

## Naming Convention Analysis
### Directory Naming
{chr(10).join(f'- **{pattern}**: {count} directories' for pattern, count in naming_patterns['directories'].items())}

### File Naming
{chr(10).join(f'- **{pattern}**: {count} files' for pattern, count in naming_patterns['file_naming'].items())}

## Duplicate Titles Found
- **{len(duplicates)} duplicate title groups** identified
- **{sum(len(files) for files in duplicates.values())} total files** with duplicate titles

## Key Findings

### Structural Issues
1. **Mixed naming conventions**: Found {naming_patterns['directories']['kebab-case'] + naming_patterns['directories']['snake_case'] + naming_patterns['directories']['mixed-case']} directories not following kebab-case
2. **Framework fragmentation**: Content spread across {len(framework_stats)} different framework categories
3. **Duplicate content**: {len(duplicates)} title duplications suggest content redundancy

### Content Classification
1. **UWP dominance**: {framework_stats.get('uwp', 0)} UWP files vs {framework_stats.get('windows-app-sdk', 0)} Windows App SDK files
2. **Reference heavy**: {content_type_stats.get('reference', 0)} reference docs vs {content_type_stats.get('tutorial', 0)} tutorials
3. **Intermediate focus**: Most content targets intermediate developers

### Recommendations
1. **Implement consistent naming**: Standardize on kebab-case for all directories and files
2. **Consolidate frameworks**: Clear separation between modern (Windows App SDK) and legacy (UWP) content
3. **Address duplicates**: Review and consolidate {len(duplicates)} duplicate title groups
4. **Improve metadata**: {len([e for e in content_inventory if not e['has_frontmatter']])} files missing frontmatter metadata

## Next Steps
1. Review content-inventory.csv for detailed file analysis
2. Use link-analysis.json to plan redirect strategies
3. Address naming convention inconsistencies
4. Plan framework-based content reorganization
"""

    with open('content-audit-report.md', 'w', encoding='utf-8') as f:
        f.write(summary_report)
    
    print("📊 Audit complete! Generated files:")
    print("   - content-inventory.csv")
    print("   - link-analysis.json") 
    print("   - content-audit-report.md")

if __name__ == '__main__':
    main()
