namespace FeatureStoreServiceSmokeTester.Controllers
{
    using System;
    using System.Collections.Generic;
    using ButtonCommandHandlers;
    using Resources;

    /// <summary>
    /// </summary>
    public static class DropDownButtonClickedCommandFactory
    {
        private static readonly Dictionary<Tuple<string, string>, IDropDownButtonCommand> CommandHandlerMappings = new Dictionary
            <Tuple<string, string>, IDropDownButtonCommand>
                                                                                                                       {
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_CHECK_FEATURE_STATE,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_SYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new SynchronousCheckFeatureStateDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_CHECK_FEATURE_STATE,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_ASYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new AsynchronousCheckFeatureStateDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_CREATE_FEATURE,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_SYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new SynchronousCreateFeatureDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_CREATE_FEATURE,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_ASYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new AsynchronousCreateFeatureDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_RETRIEVE_DEFINED_FEATURES,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_SYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new SynchronousRetrieveDefinedFeaturesDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_RETRIEVE_DEFINED_FEATURES,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_ASYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new AsynchronousRetrieveDefinedFeaturesDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_UPDATE_FEATURE_STATE,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_SYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new SynchronousUpdateFeatureStateDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               },
                                                                                                                           {
                                                                                                                               new Tuple
                                                                                                                               <
                                                                                                                               string
                                                                                                                               ,
                                                                                                                               string
                                                                                                                               >
                                                                                                                               (
                                                                                                                               ControlTextsResource
                                                                                                                                   .
                                                                                                                                   TAB_PAGE_TEXT_UPDATE_FEATURE_STATE,
                                                                                                                               CommonResources
                                                                                                                                   .
                                                                                                                                   TEXT_ASYNCHRONOUS)
                                                                                                                               ,
                                                                                                                               new AsynchronousUpdateFeatureStateDropDownButtonCommand
                                                                                                                               ()
                                                                                                                               }
                                                                                                                       };

        /// <summary>
        ///   Gets the command.
        /// </summary>
        /// <param name = "methodName"></param>
        /// <param name = "synchronicity"></param>
        /// <returns></returns>
        public static IDropDownButtonCommand GetCommand(string methodName, string synchronicity)
        {
            Tuple<string, string> key = new Tuple<string, string>(methodName, synchronicity);
            if (CommandHandlerMappings.ContainsKey(key))
            {
                return CommandHandlerMappings[key];
            }

            return null;
        }
    }
}