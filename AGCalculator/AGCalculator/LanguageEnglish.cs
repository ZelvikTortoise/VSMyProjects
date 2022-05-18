using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    sealed class LanguageEnglish : Language
    {
        //
        // Form section:
        //
        // Start of FormMainMenu:
        public override string MainMenuText { get => "Main menu"; }
        public override string LabelDesc2DText { get => "Objects in a plain (in 2D):"; }
        public override string LabelDesc3DText { get => "Objects in a space (in 3D):"; }
        public override string ButtonGoToAddText { get => "Add"; }
        public override string ButtonGoToDisplayText { get => "Display"; }
        public override string ButtonGoToRemoveText { get => "Remove"; }
        public override string ButtonGoToChangeLanguageText { get => "Change language"; }
        public override string ButtonExitText { get => "Exit"; }
        public override string MessageNoObejctsCaption { get => "No objects of requested type found"; }
        public override string MessageNoObejctsText { get => "There are no such objects in memory at the moment. Please, create some before displaying them."; }
        // End.

        // Start of FormAdd2D:
        public override string FormAdd2DText { get => "Adding objects in 2D"; }
        public override string LabelDescAdd2DText { get => "Add an object (in 2D) to the memory"; }
        public override string LabelDescChooseTypeOf2DText { get => "Choose a type of object (in 2D):"; }
        public override string LabelDescSelectInfo2DText { get => "Select the information you have:"; }
        public override string LabelDescAddName2DText { get => "Name your new object: "; }
        public override string ButtonAdd2DObjectText { get => "Add"; }
        public override string ButtonMenuFromAdd2DText { get => "Menu"; }
        public override string MessageEmptyNameText { get => "Please name your object first!"; }
        public override string MessageEmptyNameCaption { get => "Empty name"; }
        public override string MessageNameExistsText { get => "The name you've chosen is already taken. Please, rename your object."; }
        public override string MessageNameExistsCaption { get => "Name already exists"; }
        public override string MessageSureToAddObjectText { get => "Are you sure you want to create this object and add it to the calculator's memory?"; }
        public override string MessageSureToAddObjectCaption { get => "Adding new object"; }
        public override string MessageObjectAddedText { get => "The object has been succesfully added to memory."; }
        public override string MessageObjectAddedCaption { get => "Object added acknowledgement"; }
        public override string ExceptionNotImplementedText { get => "We haven't implemented this constructor of the chosen object type. Please choose a different constructor. You may contact our team at macek.mt.kgk@seznam.cz to report the bug. We apologize for any inconvenience caused by our mistakes."; }
        public override string ExceptionWrongObjectNameText { get => "It's likely that there's an error in implementation of the " + nameof(ObjectInfo.Name) + " property of the " + nameof(ObjectInfo) + " in the library of some of your objects."; }
        public override string MessageObjectCreationFailedText { get => "We are sorry but your object couldn't be created. Please, change the information and try again."; }
        public override string MessageObjectCreationFailedCaption { get => "Object creation failed"; }
        public override string MessageOriginalExceptionMessageText { get => "The cause of the error (original message):"; }
        // End.

        // Start of FormDisplay2D:
        public override string FormDisplay2DText { get => "Display objects in 2D"; }
        public override string LabelDescDisplay2DObjectText { get => "Select an object to be displayed:"; }
        public override string ButtonDisplay2DObjectText { get => "Display"; }
        public override string ButtonAdd2DFromDisplay2DText { get => "Add objects (in 2D)"; }
        public override string ButtonMenuFrom2DText { get => "Menu"; }
        public override string LabelDescTypeOf2DObjectText { get => "Type of this object:"; }
        public override string LabelDescPointOf2DObjectText { get => "Is this point a part of the selected object?"; }
        public override string ButtonCheckPoint2DText { get => "Check"; }
        public override string ButtonGoToCheckParametricEquationText { get => "Try parametric equation"; }
        public override string MessageEmptyComboBoxText { get => "First select an object to be displayed."; }
        public override string MessageEmptyComboBoxCaption { get => "No object has been selected"; }

        public override string SlopeEquationsDoesNotExist { get => "The slope equation of this line DNE."; }
        public override string SegmentalEquationsDoesNotExist { get => "The segmental equation of this line DNE."; }
        // End.

        // Start of FormRemove:
        public override string FormRemoveText { get => "Removing objects from memory"; }
        public override string RemovingIn2DText { get => "in " + Program.language.Dimension2D; }
        public override string RemovingIn3DText { get => "in " + Program.language.Dimension3D; }
        public override string LabelRemoveHeadlineText { get => "Select an object to be removed:"; }
        public override string ButtonRemoveText { get => "Remove"; }
        public override string MessageSureToRemoveObjectText { get => "Are you sure you want to remove this object from the calculator's memory?"; }
        public override string MessageSureToRemoveObjectCaption { get => "Removing an existing object"; }
        public override string MessageObjectRemovedText { get => "The object has been succesfully removed from memory."; }
        public override string MessageObjectRemovedCaption { get => "Object removed acknowledgement"; }
        public override string MessageEverythingWasRemovedText { get => "All objects (from this dimension) were already removed from memory. Add some first before removing more objects."; }
        public override string MessageEverythingWasRemovedCaption { get => "Empty memory (in this dimension)"; }
        // End.

        // Start of FormParametricCheck:
        public override string FormParametricCheckText { get => "Parametric equation check"; }
        public override string LabelParametricCheckHeadlineText { get => "Checking parametric equations equivalency"; }
        public override string LabelParametricModelText { get => "Equations' model:"; }
        public override string ExceptionUnhandledSelectedNullText { get => "Somehow user found a way to get to this form without selecting an object to be displayed first. We apologize for any inconvenience caused by the error."; }
        public override string LabelParametricCheckQuestionText { get => "Is this a set of equations of the displayed object?"; }
        public override string ButtonCheckParametricEquationText { get => "Check"; }
        public override string ExceptionObjectInterfaceTypeText { get => "Unhanled object interface type."; }
        // End.

        // Start of FormLanguage:
        public override string FormLanguageText { get => "Language selection"; }
        public override string LabelDescSelectLanguageText { get => "Select a language:"; }
        public override string ButtonSelectLanguageText { get => "Select"; }
        public override string ExceptionUnhandledLanguageText { get => "Unhandled language option, please select a different language until we fix the problem."; }
        // End.
        // 
        // End of Form section.
        // 


        //
        // General section:
        //
        // Start of Types of 2D objects.        
        public override string TypeOf2DObjectLine { get => "line"; }
        public override string TypeOf2DObjectCircle { get => "circle"; }
        public override string TypeOf2DObjectEllipse { get => "ellipse"; }
        public override string TypeOf2DObjectHyperbola { get => "hyperbola"; }
        public override string TypeOf2DObjectParabola { get => "parabola"; }
        // End.

        // Start of Information needed to create an object.
        public override string InfoTwoPoints { get => "two points"; }
        public override string InfoPointAndDirectionalVector { get => "a point and a directional vector"; }
        public override string InfoPointAndNormalVector { get => "a point and a normal vector"; }
        public override string InfoGeneralEquation { get => "its general equation"; }
        public override string InfoSlopeEquation { get => "its slope equation"; }
        public override string RadiusName { get => "radius"; }
        public override string InfoMidpointAndRadius { get => "midpoint and radius"; }
        public override string EllipseAxesLengthsName { get => "semiaxes' lengths"; }
        public override string EllipseIsCircle { get => "this ellipse is a circle"; }
        public override string InfoEllipseParameters { get => "standard equation"; }
        public override string HyperbolaAxesLengthsName { get => "semiaxes' lengths"; }
        public override string HyperbolaAxisLengthName { get => "semiaxis's length"; }
        public override string HyperbolaLinearEccentricityName { get => "linear eccentricity"; }
        public override string InfoHyperbolaParametersAB { get => "standard equation (a, b)"; }
        public override string InfoHyperbolaParametersEA { get => "standard equation (e, a)"; }
        public override string InfoHyperbolaParametersEB { get => "standard equation (e, b)"; }
        public override string ParabolaIsOpenToTheText { get => "is open to the"; }
        public override string ParabolaTopText { get => "top"; }
        public override string ParabolaBottomText { get => "bottom"; }
        public override string ParabolaRightText { get => "right"; }
        public override string ParabolaLeftText { get => "left"; }
        public override string InfoParabolaVertexAndFocus { get => "vertex and focus"; }
        //  ...
        // End.

        // Start of Answers Yes / No.
        public override string AnswerYes { get => "Yes"; }
        public override string AnswerNo { get => "No"; }
        // End.
        // 
        // End of General section.
        // 
    }
}
