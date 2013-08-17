// ****************************************************************************
// <copyright file="CheckFeatureStateTabPage.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// 
// *****************************************************************************
namespace FeatureStoreServiceSmokeTester.Controls
{
    using System.ComponentModel;

    /// <summary>
    /// Wraps the CheckFeatureState service call in a strongly-typed TabPage.
    /// </summary>
    [ToolboxItem(true)]
    public class CheckFeatureStateTabPage : BaseTabPage
    {
        public CheckFeatureStateTabPage()
            : base(Resources.ControlTextsResource.TAB_PAGE_TEXT_CHECK_FEATURE_STATE, ArgumentFieldsLayout.CheckFeatureState)
        {
        }
    }
}