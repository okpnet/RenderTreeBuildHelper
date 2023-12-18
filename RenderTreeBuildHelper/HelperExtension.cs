﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Xml.Linq;

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
        public static void AddClass(this RenderTreeBuilder builder,ref int sequence, params string[] classes)
        {
            var seq = sequence;
            if (classes.Length == 0) return;
            builder.AddAttribute(seq++, "class", string.Join(" ", classes));
            sequence = seq;
            return;
        }
        /// <summary>
        /// Close the element after the action of opening the element and inserting content.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="element"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static void OpenElementHelper(this RenderTreeBuilder builder,ref int sequence, string element, Action contents)
        {
            var seq = sequence;
            builder.OpenElement(seq++, element);
            contents.Invoke();
            builder.CloseElement();
            sequence = seq;
            return;
        }
        /// <summary>
        /// Close the component after the action of opening the component and inserting content.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static void OpenComponentHelper<TComponent>(this RenderTreeBuilder builder,ref int sequence, Action contents) where TComponent : notnull, Microsoft.AspNetCore.Components.IComponent
        {
            var seq = sequence;
            builder.OpenComponent<TComponent>(seq++);
            contents.Invoke();
            builder.CloseComponent();
            sequence = seq;
            return;
        }

        /// <summary>
        /// &U+003C span class="icon" &U+003E &U+003Ci class="gg-play-list-add"/&U+003E &U+003C/span &U+003Eを生成
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seq"></param>
        /// <param name="iconClass"></param>
        /// <returns></returns>
        public static void AddIcons(this RenderTreeBuilder builder,ref int sequence, string iconClass)
        {
            var seq = sequence;
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "icon");
            builder.OpenElement(seq++, "i");
            builder.AddAttribute(seq++, "class", iconClass);
            builder.CloseElement();
            builder.CloseElement();
            sequence = seq;
            return;
        }
        /// <summary>
        /// Optional parameter attributes.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="name"></param>
        /// <param name="addAttr"></param>
        public static void AddAttributeHelper(this RenderTreeBuilder builder, ref int sequence,string name,params string[] addAttr)
        {
            if (addAttr.Length>0 && string.Join("", addAttr).Trim() is string attrs && attrs.Length>0 )
            {
                var seq = sequence;

                builder.AddAttribute(seq++, name, attrs);
                sequence = seq;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="contentName"></param>
        /// <param name="action"></param>
        public static void AddContentsHelper(this RenderTreeBuilder builder, ref int sequence,string contentName,Action<RenderTreeBuilder> action)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, contentName,(RenderFragment)(action.Invoke));
            sequence = seq;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="action"></param>
        public static void AddChildContentsHelper(this RenderTreeBuilder builder, ref int sequence,  Action<RenderTreeBuilder> action)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, "ChildContent", (RenderFragment)(action.Invoke));
            sequence = seq;
        }
    }
}
