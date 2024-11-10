using UnityEditor;
using UnityEngine;

namespace Toolbox.Editor.Drawers
{
    using UnityGraphicsSettings = UnityEngine.Rendering.GraphicsSettings;
    
    public sealed class ShowForRenderPipelineDrawer : ToolboxConditionDrawer<ShowForRenderPipelineAttribute>
    {
        protected override PropertyCondition OnGuiValidateSafe(SerializedProperty property, ShowForRenderPipelineAttribute attribute)
        {
            return FindActiveRenderingPipeline() == attribute.PipelineType ? PropertyCondition.Valid : PropertyCondition.NonValid;
        }
        
        private static ShowForRenderPipelineAttribute.Type FindActiveRenderingPipeline()
        {
            if (UnityGraphicsSettings.renderPipelineAsset != null)
            {
                var srpType = UnityGraphicsSettings.renderPipelineAsset.GetType().ToString();
                if (srpType.Contains("HDRenderPipelineAsset"))
                    return ShowForRenderPipelineAttribute.Type.Hdrp;
        
                if (srpType.Contains("UniversalRenderPipelineAsset") || srpType.Contains("LightweightRenderPipelineAsset"))
                    return ShowForRenderPipelineAttribute.Type.Urp;
            }
            
            return ShowForRenderPipelineAttribute.Type.BuiltIn;
        }
    }
}