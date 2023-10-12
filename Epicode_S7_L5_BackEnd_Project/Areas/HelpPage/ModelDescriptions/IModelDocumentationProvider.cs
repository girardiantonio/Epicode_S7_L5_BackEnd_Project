using System;
using System.Reflection;

namespace Epicode_S7_L5_BackEnd_Project.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}