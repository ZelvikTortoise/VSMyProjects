using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    abstract public class Language
    {
        //
        // Start of languages to be used by the program.
        //
        public readonly static Language english = new LanguageEnglish();
        public readonly static Language czech = new LanguageCzech();
        //
        // End of languages.
        //

        //
        // Form section:
        //
        // Start of FormMainMenu:
        public abstract string MainMenuText { get; }
        public abstract string LabelDesc2DText { get; }
        public abstract string LabelDesc3DText { get; }
        public abstract string ButtonGoToAddText { get; }
        public abstract string ButtonGoToDisplayText { get; }
        public abstract string ButtonGoToRemoveText { get; }
        public abstract string ButtonGoToChangeLanguageText { get; }
        public abstract string ButtonExitText { get; }
        public abstract string MessageNoObejctsCaption { get; }
        public abstract string MessageNoObejctsText { get; }
        // End.

        // Start of FormAdd2D:
        public abstract string FormAdd2DText { get; }
        public abstract string LabelDescAdd2DText { get; }
        public abstract string LabelDescChooseTypeOf2DText { get; }
        public abstract string LabelDescSelectInfo2DText { get; }
        public abstract string LabelDescAddName2DText { get; }
        public abstract string ButtonAdd2DObjectText { get; }
        public abstract string ButtonMenuFromAdd2DText { get; }
        public abstract string MessageEmptyNameText { get; }
        public abstract string MessageEmptyNameCaption { get; }
        public abstract string MessageNameExistsText { get; }
        public abstract string MessageNameExistsCaption { get; }
        public abstract string MessageSureToAddObjectText { get; }
        public abstract string MessageSureToAddObjectCaption { get; }
        public abstract string MessageObjectAddedText { get; }
        public abstract string MessageObjectAddedCaption { get; }
        public abstract string ExceptionNotImplementedText { get; }
        public abstract string ExceptionWrongObjectNameText { get; }
        public abstract string MessageObjectCreationFailedText { get; }
        public abstract string MessageObjectCreationFailedCaption { get; }
        public abstract string MessageOriginalExceptionMessageText { get; }
        // End.

        // Start of FormDisplay2D:
        public abstract string FormDisplay2DText { get; }
        public abstract string LabelDescDisplay2DObjectText { get; }
        public abstract string ButtonDisplay2DObjectText { get; }
        public abstract string ButtonAdd2DFromDisplay2DText { get; }
        public abstract string ButtonMenuFrom2DText { get; }
        public abstract string LabelDescTypeOf2DObjectText { get; }
        public abstract string LabelDescPointOf2DObjectText { get; }
        public abstract string ButtonCheckPoint2DText { get; }
        public abstract string ButtonGoToCheckParametricEquationText { get; }
        public abstract string MessageEmptyComboBoxText { get; }
        public abstract string MessageEmptyComboBoxCaption { get; }

        public abstract string SlopeEquationsDoesNotExist { get; }
        public abstract string SegmentalEquationsDoesNotExist { get; }
        // End.

        // Start of FormRemove:
        public virtual string Dimension2D { get => "2D"; }
        public virtual string Dimension3D { get => "3D"; }
        public abstract string FormRemoveText { get; }
        public abstract string RemovingIn2DText { get; }
        public abstract string RemovingIn3DText { get; }
        public abstract string LabelRemoveHeadlineText { get; }
        public abstract string ButtonRemoveText { get; }
        public abstract string MessageSureToRemoveObjectText { get; }
        public abstract string MessageSureToRemoveObjectCaption { get; }
        public abstract string MessageObjectRemovedText { get; }
        public abstract string MessageObjectRemovedCaption { get; }
        public abstract string MessageEverythingWasRemovedText { get; }
        public abstract string MessageEverythingWasRemovedCaption { get; }
        // End.

        // Start of FormParametricCheck:
        public abstract string FormParametricCheckText { get; }
        public abstract string LabelParametricCheckHeadlineText { get; }
        public abstract string LabelParametricModelText { get; }
        public abstract string ExceptionUnhandledSelectedNullText { get; }
        public abstract string LabelParametricCheckQuestionText { get; }
        public abstract string ButtonCheckParametricEquationText { get; }
        public abstract string ExceptionObjectInterfaceTypeText { get; }
        // End.

        // Start of FormLanguage:
        public abstract string FormLanguageText { get; }
        public abstract string LabelDescSelectLanguageText { get; }
        public abstract string ButtonSelectLanguageText { get; }
        public abstract string ExceptionUnhandledLanguageText { get; }
        // End.
        // 
        // End of Form section.
        // 


        //
        // General section:
        //
        // Start of Types of 2D objects.
        public List<string> GetTypesOf2DObjects()
        {
            List<string> list = new List<string>
            {
                TypeOf2DObjectLine,
                TypeOf2DObjectCircle,
                TypeOf2DObjectEllipse,
                TypeOf2DObjectHyperbola,
                TypeOf2DObjectParabola
            };
            return list;
        }
        public abstract string TypeOf2DObjectLine { get; }
        public abstract string TypeOf2DObjectCircle { get; }
        public abstract string TypeOf2DObjectEllipse { get; }
        public abstract string TypeOf2DObjectHyperbola { get; }
        public abstract string TypeOf2DObjectParabola { get; }
        // End.

        // Start of Information needed to create an object.
        // Language only.
        public List<string> GetListInfoNeededLine2D()
        {
            List<string> list = new List<string>
            {
                InfoTwoPoints,
                InfoPointAndDirectionalVector,
                InfoPointAndNormalVector,
                InfoGeneralEquation,
                InfoSlopeEquation
            };

            return list;
        }
        public List<string> GetListInfoNeededCircle2D()
        {
            return new List<string>
            {
                InfoMidpointAndRadius,
                InfoGeneralEquation
            };
        }
        public List<string> GetListInfoNeededEllipse2D()
        {
            return new List<string>
            {
                 InfoEllipseParameters
            };
        }
        public List<string> GetListInfoNeededHyperbola2D()
        {
            return new List<string>
            {
                InfoHyperbolaParametersAB,
                InfoHyperbolaParametersEA,
                InfoHyperbolaParametersEB
            };
        }
        public List<string> GetListInfoNeededParabola2D()
        {
            return new List<string>
            {
                InfoParabolaVertexAndFocus
            };
        }
        // ... 

        // Language abstract.
        public abstract string InfoTwoPoints { get; }
        public abstract string InfoPointAndDirectionalVector { get; }
        public abstract string InfoPointAndNormalVector { get; }
        public abstract string InfoGeneralEquation { get; }
        public abstract string InfoSlopeEquation { get; }
        public abstract string RadiusName { get; }
        public abstract string InfoMidpointAndRadius { get; }
        public abstract string EllipseAxesLengthsName { get; }
        public abstract string EllipseIsCircle { get; }
        public abstract string InfoEllipseParameters { get; }
        public abstract string HyperbolaAxesLengthsName { get; }
        public abstract string HyperbolaAxisLengthName { get; }
        public abstract string HyperbolaLinearEccentricityName { get; }
        public abstract string InfoHyperbolaParametersAB { get; }
        public abstract string InfoHyperbolaParametersEA { get; }
        public abstract string InfoHyperbolaParametersEB { get; }
        public abstract string ParabolaIsOpenToTheText { get; }
        public abstract string ParabolaTopText { get; }
        public abstract string ParabolaBottomText { get; }
        public abstract string ParabolaRightText { get; }
        public abstract string ParabolaLeftText { get; }
        public abstract string InfoParabolaVertexAndFocus { get; }
        // ...
        // End.

        // Start of Types of 3D objects.
        public List<string> GetTypesOf3DObjects()
        {
            return new List<string>();
                //throw new NotImplementedException();
        }   // (3D: TODO)
        // ...
        // End.

        // Start of national symbols.
        public char DecimalPointText { get => ','; }
        // End.

        // Start of Answers Yes / No.
        public abstract string AnswerYes { get; }
        public abstract string AnswerNo { get; }
        // End.
        // 
        // End of General section.
        // 
    }
}
