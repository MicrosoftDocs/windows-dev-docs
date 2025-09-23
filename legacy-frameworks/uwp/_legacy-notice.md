# ⚠️ UWP Legacy Framework Notice

> **IMPORTANT**: Universal Windows Platform (UWP) is now in **legacy status**. While existing UWP applications continue to be supported, **UWP is not recommended for new projects**.

## Current Status

| Aspect | Status | Details |
|--------|--------|---------|
| **New Development** | ❌ **Not Recommended** | Use [Windows App SDK](../../modern-windows-apps/windows-app-sdk/) instead |
| **Existing Apps** | ✅ **Supported** | Continued support through standard lifecycle |
| **Security Updates** | ✅ **Available** | Critical security fixes provided |
| **Bug Fixes** | ⚠️ **Limited** | Critical issues only, no general bug fixes |
| **New Features** | ❌ **None** | No new UWP features being developed |
| **Store Support** | ✅ **Continued** | Existing UWP apps can still publish updates |

## Why UWP is Legacy

### Microsoft's Official Position
Microsoft has officially transitioned Windows development focus to the **Windows App SDK** (formerly Project Reunion). UWP development has been discontinued in favor of this modern, unified platform.

### Technical Limitations
- **Limited Windows version support** (Windows 10 only)
- **Store-only deployment** restrictions
- **Reduced desktop integration** capabilities
- **Performance limitations** compared to modern alternatives
- **Limited ecosystem** with fewer third-party libraries

### Strategic Direction
Microsoft is investing all new Windows development efforts in the Windows App SDK, which provides:
- **Broader compatibility** (Windows 10 1809+ and Windows 11)
- **Flexible deployment** (Store and traditional installation)
- **Better performance** and resource utilization
- **Enhanced desktop integration**
- **Future-proof architecture**

## Migration Path

### ✅ Recommended: Migrate to Windows App SDK

**Benefits of Migration:**
- **Future-proof development** with active feature development
- **Better performance** and user experience
- **Flexible deployment options** (not limited to Store)
- **Enhanced Windows integration** and capabilities
- **Larger ecosystem** and community support
- **Modern development tools** and debugging experience

**Migration Resources:**
- **[UWP to Windows App SDK Migration Guide](../../modern-windows-apps/windows-app-sdk/migration-guides/from-uwp.md)**
- **[API Mapping Reference](../../modern-windows-apps/windows-app-sdk/migration-guides/uwp-api-mapping.md)**
- **[Migration Assessment Tool](../migration-assessment-guide.md)**
- **[Step-by-Step Migration Process](../ui-migration-guide.md)**

### 🛠️ Migration Support

**Microsoft Provided Tools:**
- **[.NET Upgrade Assistant](https://dotnet.microsoft.com/platform/upgrade-assistant)** - Automated migration assistance
- **[Windows App SDK Documentation](https://docs.microsoft.com/windows/apps/windows-app-sdk/)** - Complete modern development guide
- **[Migration Samples](https://github.com/microsoft/WindowsAppSDK-Samples)** - Real migration examples

**Community Resources:**
- **[Migration Forum](https://github.com/microsoft/WindowsAppSDK/discussions/categories/migration)** - Community support
- **[Stack Overflow](https://stackoverflow.com/questions/tagged/windows-app-sdk+uwp)** - Technical migration questions
- **[Windows Developer Blog](https://blogs.windows.com/windowsdeveloper/)** - Latest migration guidance

## When to Continue with UWP

### ⚠️ Limited Scenarios Only

**Consider staying with UWP only if:**
- Your app is in **maintenance-only mode** with no new feature development
- Migration costs **significantly outweigh benefits** for your specific case
- You have **critical dependencies** that cannot be replaced or updated
- **Regulatory/organizational constraints** prevent framework changes
- Your app requires **Xbox development** (though Xbox also supports modern alternatives)

### 📋 UWP Maintenance Guidelines

**If continuing with UWP:**
1. **Minimize new feature development** to reduce technical debt
2. **Plan migration timeline** for when maintenance becomes unsustainable
3. **Monitor breaking changes** in Windows updates affecting UWP
4. **Document migration requirements** for future team planning
5. **Consider hybrid approach** with new features in Windows App SDK

## UWP Documentation Usage

### 📚 How to Use This Legacy Documentation

**This UWP documentation is preserved for:**
- **Maintaining existing UWP applications**
- **Understanding UWP concepts** during migration planning
- **Reference during migration** to Windows App SDK
- **Legacy code support** and troubleshooting

**Important Reminders:**
- ⚠️ **Do not use for new projects** - Always start new projects with Windows App SDK
- 🔄 **Cross-reference with migration guides** when making changes
- 📅 **Plan migration timeline** to avoid long-term technical debt
- 🆘 **Get help with migration** through community and official resources

### 🗂️ Documentation Organization

This UWP documentation maintains the original structure for familiarity:
- **[Getting Started](./getting-started/)** - UWP basics (legacy reference only)
- **[App Development](./develop/)** - UWP features and capabilities
- **[UI and Input](./ui-input/)** - UWP user interface framework
- **[Platform Features](./platform-features/)** - Windows platform integration
- **[Deployment](./packaging/)** - UWP app packaging and distribution

## 🚨 Critical Reminders

### For Development Teams
- **Update your development roadmap** to include Windows App SDK migration
- **Train team members** on modern Windows development approaches  
- **Budget migration time** in your project planning
- **Evaluate third-party dependencies** for Windows App SDK compatibility

### For New Developers
- **Start with [Windows App SDK](../../modern-windows-apps/windows-app-sdk/)** for any new Windows development
- **Use this UWP documentation** only for understanding existing code
- **Focus learning time** on modern Windows development approaches
- **Join Windows App SDK community** for current best practices

---

**UWP Status**: ⚠️ **Legacy Framework** | **Not Recommended for New Projects** | **Migration to Windows App SDK Recommended**

**Last Updated**: September 2024 | **Migration Support Available**: ✅ | **Future UWP Updates**: ❌

*For modern Windows development, see [Windows App SDK Documentation](../../modern-windows-apps/windows-app-sdk/). For migration assistance, see [Migration Guides](../migration-assessment-guide.md).*
