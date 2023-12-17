using Microsoft.AspNetCore.Components.Rendering;

namespace RenderTreeBuildHelper
{
    public static class HelperExtension
    {
        /// <summary>
        /// add class
        /// </summary>
        internal const string ATTR_CLASS = "class";
        /// <summary>
        /// Add a class to the element.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seq"></param>
        /// <param name="classes"></param>
        /// <returns></returns>
        public static int AddClass(this RenderTreeBuilder builder,int sequence, params string[] classes)
        {
            var seq = sequence;
            if (classes.Length == 0) return seq;
            builder.AddAttribute(seq++, "class", string.Join(" ", classes));
            return seq;
        }
        /// <summary>
        /// Close the element after the action of opening the element and inserting content.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="element"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static int OpenElementHelper(this RenderTreeBuilder builder, int sequence, string element, Action contents)
        {
            var seq = sequence;
            builder.OpenElement(seq++, element);
            contents.Invoke();
            builder.CloseElement();
            return seq;
        }
        /// <summary>
        /// Close the component after the action of opening the component and inserting content.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static int OpenComponentHelper<TComponent>(this RenderTreeBuilder builder, int sequence, Action contents) where TComponent : notnull, Microsoft.AspNetCore.Components.IComponent
        {
            var seq = sequence;
            builder.OpenComponent<TComponent>(seq++);
            contents.Invoke();
            builder.CloseComponent();
            return seq;
        }

        /// <summary>
        /// &U+003C span class="icon" &U+003E &U+003Ci class="gg-play-list-add"/&U+003E &U+003C/span &U+003Eを生成
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seq"></param>
        /// <param name="iconClass"></param>
        /// <returns></returns>
        public static int AddIcons(this RenderTreeBuilder builder, int sequence, string iconClass)
        {
            var seq = sequence;
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "icon");
            builder.OpenElement(seq++, "i");
            builder.AddAttribute(seq++, "class", iconClass);
            builder.CloseElement();
            builder.CloseElement();
            return seq;
        }
    }
}
