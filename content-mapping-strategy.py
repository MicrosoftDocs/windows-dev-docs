#!/usr/bin/env python3
"""
Content Mapping Strategy for Windows Development Documentation Restructure
Maps current content locations to proposed new structure
"""

import csv
import json
from collections import defaultdict, Counter

def load_content_inventory():
    """Load the content inventory from CSV"""
    inventory = []
    with open('content-inventory.csv', 'r', encoding='utf-8') as f:
        reader = csv.DictReader(f)
        inventory = list(reader)
    return inventory

def map_to_new_structure(entry):
    """Map a content entry to the proposed new structure"""
    filepath = entry['filepath']
    framework = entry['framework']
    content_type = entry['content_type']
    directory = entry['directory']
    
    # Proposed new location
    new_location = None
    category = None
    reason = None
    
    # Getting Started content
    if any(term in filepath.lower() for term in ['getting-started', 'get-started', 'hello-world', 'first-app']):
        if framework == 'uwp':
            new_location = 'legacy-frameworks/uwp/getting-started/'
            category = 'legacy-getting-started'
            reason = 'UWP getting started content moved to legacy section'
        else:
            new_location = 'getting-started/first-app-tutorials/'
            category = 'getting-started'
            reason = 'Modern getting started content'
    
    # Framework-specific routing
    elif framework == 'windows-app-sdk':
        if 'winui' in filepath.lower():
            new_location = 'modern-windows-apps/winui/'
            category = 'modern-winui'
        else:
            new_location = 'modern-windows-apps/windows-app-sdk/'
            category = 'modern-windows-app-sdk'
        reason = 'Modern Windows development content'
    
    elif framework == 'uwp':
        new_location = f'legacy-frameworks/uwp/{directory.replace("uwp/", "")}/'
        category = 'legacy-uwp'
        reason = 'UWP content preserved in legacy section'
    
    elif framework == 'wpf-winforms':
        new_location = 'legacy-frameworks/wpf-winforms/'
        category = 'legacy-wpf-winforms'  
        reason = 'Legacy .NET Framework content'
    
    elif framework == 'win32':
        new_location = 'legacy-frameworks/win32/'
        category = 'legacy-win32'
        reason = 'Native Windows API content'
    
    # Content type-based routing for unknown frameworks
    elif framework == 'unknown':
        if 'dev-environment' in filepath.lower() or 'tools' in filepath.lower():
            new_location = 'development-tools/'
            category = 'development-tools'
            reason = 'Development environment and tooling content'
        elif 'deploy' in filepath.lower() or 'packaging' in filepath.lower():
            new_location = 'deployment-distribution/'
            category = 'deployment'
            reason = 'Deployment and distribution content'
        elif 'gaming' in filepath.lower():
            new_location = 'specialized-scenarios/gaming/'
            category = 'specialized-gaming'
            reason = 'Game development content'
        elif 'iot' in filepath.lower():
            new_location = 'specialized-scenarios/iot-embedded/'
            category = 'specialized-iot'
            reason = 'IoT and embedded content'
        elif 'enterprise' in filepath.lower():
            new_location = 'specialized-scenarios/enterprise-apps/'
            category = 'specialized-enterprise'
            reason = 'Enterprise application content'
        elif content_type == 'reference' or 'api' in filepath.lower():
            new_location = 'api-reference/'
            category = 'api-reference'
            reason = 'API reference and technical documentation'
        elif any(term in filepath.lower() for term in ['community', 'contribute', 'learn']):
            new_location = 'community-resources/'
            category = 'community'
            reason = 'Community and learning resources'
        else:
            new_location = 'platform-features/'
            category = 'platform-features'
            reason = 'General Windows platform features (needs manual review)'
    
    # Default fallback
    if not new_location:
        new_location = 'platform-features/needs-review/'
        category = 'needs-manual-review'
        reason = 'Requires manual categorization'
    
    return {
        'new_location': new_location,
        'category': category,
        'reason': reason
    }

def create_migration_plan(inventory):
    """Create detailed migration plan with statistics"""
    migration_plan = []
    category_stats = Counter()
    migration_stats = defaultdict(list)
    
    for entry in inventory:
        mapping = map_to_new_structure(entry)
        
        plan_entry = {
            'current_path': entry['filepath'],
            'current_directory': entry['directory'],
            'filename': entry['filename'],
            'title': entry['title'],
            'framework': entry['framework'],
            'content_type': entry['content_type'],
            'word_count': int(entry['word_count']),
            'new_location': mapping['new_location'],
            'category': mapping['category'],
            'reason': mapping['reason']
        }
        
        migration_plan.append(plan_entry)
        category_stats[mapping['category']] += 1
        migration_stats[mapping['category']].append(plan_entry)
    
    return migration_plan, category_stats, migration_stats

def generate_redirect_rules(migration_plan):
    """Generate redirect rules for .openpublishing.redirection.json"""
    redirects = []
    
    for entry in migration_plan:
        # Skip files that are our new planning documents
        if entry['current_path'].startswith('./documentation-'):
            continue
        if entry['current_path'] == './README-documentation-restructure.md':
            continue
        if entry['current_path'] == './content-audit-script.py':
            continue
            
        current_web_path = entry['current_path'].replace('./hub/', '/windows/').replace('./uwp/', '/windows/uwp/').replace('.md', '')
        new_web_path = f"/windows/{entry['new_location']}{entry['filename'].replace('.md', '')}"
        
        redirects.append({
            "source_path": current_web_path,
            "redirect_url": new_web_path,
            "redirect_document_id": False
        })
    
    return redirects

def main():
    """Main content mapping function"""
    print("🗺️ Creating Content Mapping Strategy...")
    
    # Load inventory
    print("📋 Loading content inventory...")
    inventory = load_content_inventory()
    
    # Create migration plan
    print("🎯 Mapping content to new structure...")
    migration_plan, category_stats, migration_stats = create_migration_plan(inventory)
    
    # Save detailed migration plan
    print("💾 Saving detailed migration plan...")
    with open('content-migration-plan.csv', 'w', newline='', encoding='utf-8') as f:
        if migration_plan:
            fieldnames = migration_plan[0].keys()
            writer = csv.DictWriter(f, fieldnames=fieldnames)
            writer.writeheader()
            writer.writerows(migration_plan)
    
    # Generate redirect rules
    print("🔄 Generating redirect rules...")
    redirects = generate_redirect_rules(migration_plan)
    
    with open('redirect-rules.json', 'w', encoding='utf-8') as f:
        json.dump(redirects, f, indent=2)
    
    # Create summary report
    total_files = len(migration_plan)
    total_words = sum(entry['word_count'] for entry in migration_plan)
    
    summary_report = f"""# Content Mapping Strategy Report

## Migration Overview
- **Total files to migrate**: {total_files:,}
- **Total content words**: {total_words:,}
- **Migration categories**: {len(category_stats)}

## Migration Category Distribution
{chr(10).join(f'- **{category}**: {count:,} files ({count/total_files*100:.1f}%)' for category, count in category_stats.most_common())}

## Phase-Based Migration Recommendations

### Phase 1: Foundation and Modern Content
**Target Categories**: modern-windows-app-sdk, modern-winui, getting-started
- **Files**: {category_stats['modern-windows-app-sdk'] + category_stats.get('modern-winui', 0) + category_stats.get('getting-started', 0):,}
- **Priority**: Highest - establishes new modern development paths

### Phase 2: Legacy Content Organization
**Target Categories**: legacy-uwp, legacy-wpf-winforms, legacy-win32
- **Files**: {category_stats.get('legacy-uwp', 0) + category_stats.get('legacy-wpf-winforms', 0) + category_stats.get('legacy-win32', 0):,}
- **Priority**: High - preserves existing content with clear legacy status

### Phase 3: Specialized Content and Polish
**Target Categories**: specialized-*, api-reference, community
- **Files**: {sum(count for cat, count in category_stats.items() if cat.startswith('specialized-') or cat in ['api-reference', 'community']):,}
- **Priority**: Medium - consolidates specialized scenarios

### Phase 4: Platform Features and Review
**Target Categories**: platform-features, needs-manual-review
- **Files**: {category_stats.get('platform-features', 0) + category_stats.get('needs-manual-review', 0):,}
- **Priority**: Low - requires manual review and categorization

## Critical Migration Statistics

### Content Volume by Category
{chr(10).join(f'- **{category}**: {sum(entry["word_count"] for entry in migration_stats[category]):,} words' for category in category_stats.most_common(10))}

### High-Impact Migrations
1. **UWP Legacy Migration**: {category_stats.get('legacy-uwp', 0):,} files need clear legacy indicators
2. **Modern Content Consolidation**: {category_stats.get('modern-windows-app-sdk', 0):,} Windows App SDK files need promotion
3. **Unknown Content Review**: {category_stats.get('needs-manual-review', 0):,} files need manual categorization

## Implementation Priorities

### Immediate Actions Required
1. **Review needs-manual-review category** ({category_stats.get('needs-manual-review', 0)} files)
2. **Plan UWP legacy indicators** ({category_stats.get('legacy-uwp', 0)} files)
3. **Consolidate modern Windows App SDK content** ({category_stats.get('modern-windows-app-sdk', 0)} files)

### Redirect Strategy
- **{len(redirects):,} redirect rules** generated
- **Covers all migrated content** to prevent broken links
- **Ready for .openpublishing.redirection.json** implementation

## Quality Assurance Checkpoints

### Pre-Migration Validation
- [ ] Review all 'needs-manual-review' categorizations
- [ ] Validate redirect rules for critical pages
- [ ] Confirm new directory structure creation
- [ ] Test sample content migrations

### Post-Migration Validation  
- [ ] Verify all redirects function correctly
- [ ] Confirm no broken internal links
- [ ] Validate search functionality
- [ ] Test user navigation paths

## Next Steps
1. **Review content-migration-plan.csv** for detailed file-by-file mapping
2. **Examine redirect-rules.json** for redirect implementation
3. **Address high-priority manual reviews** before starting migration
4. **Begin Phase 1 implementation** with foundation and modern content

This mapping strategy provides a clear path from the current fragmented structure to the proposed organized, AI-friendly documentation repository.
"""

    with open('content-mapping-report.md', 'w', encoding='utf-8') as f:
        f.write(summary_report)
    
    print("✅ Content mapping complete! Generated files:")
    print("   - content-migration-plan.csv")
    print("   - redirect-rules.json")
    print("   - content-mapping-report.md")
    
    print(f"\n📊 Quick Summary:")
    print(f"   - {total_files:,} files mapped to new structure")
    print(f"   - {len(category_stats)} migration categories identified")
    print(f"   - {len(redirects):,} redirect rules generated")
    print(f"   - Top categories: {', '.join([f'{cat} ({count})' for cat, count in category_stats.most_common(3)])}")

if __name__ == '__main__':
    main()
