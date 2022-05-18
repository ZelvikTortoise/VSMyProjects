using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    sealed class LanguageCzech : Language
    {
        //
        // Form section:
        //
        // Start of FormMainMenu:
        public override string MainMenuText { get => "Hlavní nabídka"; }
        public override string LabelDesc2DText { get => "Objekty v rovině (ve 2D):"; }
        public override string LabelDesc3DText { get => "Objekty v prostoru (ve 3D):"; }
        public override string ButtonGoToAddText { get => "Přidat"; }
        public override string ButtonGoToDisplayText { get => "Zobrazit"; }
        public override string ButtonGoToRemoveText { get => "Odstranit"; }
        public override string ButtonGoToChangeLanguageText { get => "Změnit jazyk"; }
        public override string ButtonExitText { get => "Ukončit"; }
        public override string MessageNoObejctsCaption { get => "Žádné objekty požadovaného typu nebyly nalezeny"; }
        public override string MessageNoObejctsText { get => "V paměti nejsou uloženy žádné objekty tohoto typu. Prosím, vytvořte nějaké před tím, než se je pokusíte zobrazit."; }
        // End.

        // Start of FormAdd2D:
        public override string FormAdd2DText { get => "Přidávání objektů ve 2D"; }
        public override string LabelDescAdd2DText { get => "Přidejte do paměti nový objekt (ve 2D)"; }
        public override string LabelDescChooseTypeOf2DText { get => "Zvolte si typ objektu (ve 2D):"; }
        public override string LabelDescSelectInfo2DText { get => "Vyberte, které informace znáte:"; }
        public override string LabelDescAddName2DText { get => "Pojmenujte si váš objekt: "; }
        public override string ButtonAdd2DObjectText { get => "Přidat"; }
        public override string ButtonMenuFromAdd2DText { get => "Menu"; }
        public override string MessageEmptyNameText { get => "Nejdříve vás objekt pojmenujte!"; }
        public override string MessageEmptyNameCaption { get => "Prázdné jméno"; }
        public override string MessageNameExistsText { get => "Toto jméno je již obsazeno. Prosím, přejmenujte vás objekt."; }
        public override string MessageNameExistsCaption { get => "Jméno objektu už existuje"; }
        public override string MessageSureToAddObjectText { get => "Jste si jistý(á), že chcete tento objekt vytvořit a následně ho přidat do paměti kalkulačky?"; }
        public override string MessageSureToAddObjectCaption { get => "Přidávání nového objektu"; }
        public override string MessageObjectAddedText { get => "Objekt byl úspěšně přidán do paměti."; }
        public override string MessageObjectAddedCaption { get => "Potvrzení přidání objektu"; }
        public override string ExceptionNotImplementedText { get => "Vámi vybraný konstruktor vybraného typu objektu nebyl implementován! Prosím, zvolte jiný konstruktor. Pro nahlášení bugu můžete kontaktovat náš tým na adrese macek.mt.kgk@seznam.cz. Za způsobené nepříjemnosti se omlouváme."; }
        public override string ExceptionWrongObjectNameText { get => "Pravděpodobně je chyba v pojmenování vlastnosti " + nameof(ObjectInfo.Name) + " třídy " + nameof(ObjectInfo) + " v knihovně některého z objektů."; }
        public override string MessageObjectCreationFailedText { get => "Je nám líto, ale váš objekt není možné vytvořit. Změňte zadané údaje a zkuste to znovu."; }
        public override string MessageObjectCreationFailedCaption { get => "Vytvoření objektu selhalo"; }
        public override string MessageOriginalExceptionMessageText { get => "Důvod chyby (originální zpráva):"; }
        // End.

        // Start of FormDisplay2D:
        public override string FormDisplay2DText { get => "Zobrazení objektů ve 2D"; }
        public override string LabelDescDisplay2DObjectText { get => "Vyberte objekt k zobrazení:"; }
        public override string ButtonDisplay2DObjectText { get => "Zobraz"; }
        public override string ButtonAdd2DFromDisplay2DText { get => "Přidat objekty (ve 2D)"; }
        public override string ButtonMenuFrom2DText { get => "Menu"; }
        public override string LabelDescTypeOf2DObjectText { get => "Typ tohoto objektu:"; }
        public override string LabelDescPointOf2DObjectText { get => "Leží tento bod na daném objektu?"; }
        public override string ButtonCheckPoint2DText { get => "Zkusit"; }
        public override string ButtonGoToCheckParametricEquationText { get => "Zkusit param. rovnici"; }
        public override string MessageEmptyComboBoxText { get => "Nejprve si zvolte objekt, který má být zobrazen."; }
        public override string MessageEmptyComboBoxCaption { get => "Nebyl vybrán žádný objekt k zobrazení"; }

        public override string SlopeEquationsDoesNotExist { get => "Směrnicový tvar rovnice přímky neexistuje."; }
        public override string SegmentalEquationsDoesNotExist { get => "Úsekový tvar rovnice přímky neexistuje."; }
        // End.

        // Start of FormRemove:
        public override string FormRemoveText { get => "Odebírání objektů z paměti"; }
        public override string RemovingIn2DText { get => "ve " + Program.language.Dimension2D; }
        public override string RemovingIn3DText { get => "ve " + Program.language.Dimension3D; }
        public override string LabelRemoveHeadlineText { get => "Vyberte objekt, který má být odebrán:"; }
        public override string ButtonRemoveText { get => "Odeber"; }
        public override string MessageSureToRemoveObjectText { get => "Jste si jistý(á), že chcete tento objekt odebrat z paměti kalkulačky?"; }
        public override string MessageSureToRemoveObjectCaption { get => "Odebírání objektu"; }
        public override string MessageObjectRemovedText { get => "Objekt byl úspěšně odebrán z paměti."; }
        public override string MessageObjectRemovedCaption { get => "Potvrzení odebrání objektu"; }
        public override string MessageEverythingWasRemovedText { get => "Všechny objekty (této dimenze) již byly z paměti odebrány. Vytvořte nějaké před tím, než budete dál odebírat."; }
        public override string MessageEverythingWasRemovedCaption { get => "Paměť (této dimenze) je prázdná"; }
        // End.

        // Start of FormParametricCheck:
        public override string FormParametricCheckText { get => "Zkouška parametrických rovnic"; }
        public override string LabelParametricCheckHeadlineText { get => "Zkoušení ekvivalence parametrických rovnic"; }
        public override string LabelParametricModelText { get => "Model rovnic:"; }
        public override string ExceptionUnhandledSelectedNullText { get => "Nějakým způsobem se uživatel dostal do tohoto formuláře bez toho, aniž by si nejprve vybral objekt, který má být zobrazen. Za způsobené potíže se omlouváme."; }
        public override string LabelParametricCheckQuestionText { get => "Jedná se o sadu rovnic zobrazeného objektu?"; }
        public override string ButtonCheckParametricEquationText { get => "Zkusit"; }
        public override string ExceptionObjectInterfaceTypeText { get => "Neošetřený typ interfacu objektu."; }
        // End.

        // Start of FormLanguage:
        public override string FormLanguageText { get => "Výběr jazyka"; }
        public override string LabelDescSelectLanguageText { get => "Vyberte si jazyk:"; }
        public override string ButtonSelectLanguageText { get => "Vybrat"; }
        public override string ExceptionUnhandledLanguageText { get => "Neošetřená výjimka výběru jazyka. Prosím zvolte si jiný jazyk, dokud se nám nepodaří chybu napravit."; }
        // End.
        // 
        // End of Form section.
        // 


        //
        // General section:
        //
        // Start of Types of 2D objects.
        public override string TypeOf2DObjectLine { get => "přímka"; }
        public override string TypeOf2DObjectCircle { get => "kružnice"; }
        public override string TypeOf2DObjectEllipse { get => "elipsa"; }
        public override string TypeOf2DObjectHyperbola { get => "hyperbola"; }
        public override string TypeOf2DObjectParabola { get => "parabola"; }
        // End.

        // Start of Information needed to create an object.
        public override string InfoTwoPoints { get => "dva body"; }
        public override string InfoPointAndDirectionalVector { get => "bod a směrový vektor"; }
        public override string InfoPointAndNormalVector { get => "bod a normálový vektor"; }
        public override string InfoGeneralEquation { get => "obecná rovnice"; }
        public override string InfoSlopeEquation { get => "směrnicový tvar rovnice"; }
        public override string RadiusName { get => "poloměr"; }
        public override string InfoMidpointAndRadius { get => "střed a poloměr"; }
        public override string EllipseAxesLengthsName { get => "délky poloos"; }
        public override string EllipseIsCircle { get => "tato elipsa je kružnicí"; }
        public override string InfoEllipseParameters { get => "středový tvar rovnice"; }
        public override string HyperbolaAxesLengthsName { get => "délky poloos"; }
        public override string HyperbolaAxisLengthName { get => "délka poloosy"; }
        public override string HyperbolaLinearEccentricityName { get => "excentricita"; }
        public override string InfoHyperbolaParametersAB { get => "středový tvar (a, b)"; }
        public override string InfoHyperbolaParametersEA { get => "středový tvar (e, a)"; }
        public override string InfoHyperbolaParametersEB { get => "středový tvar (e, b)"; }
        public override string ParabolaIsOpenToTheText { get => "je otevřena směrem"; }
        public override string ParabolaTopText { get => "nahoru"; }
        public override string ParabolaBottomText { get => "dolů"; }
        public override string ParabolaRightText { get => "doprava"; }
        public override string ParabolaLeftText { get => "doleva"; }
        public override string InfoParabolaVertexAndFocus { get => "vrchol a ohnisko"; }
        //  ...
        // End.

        // Start of Answers Yes / No.
        public override string AnswerYes { get => "Ano"; }
        public override string AnswerNo { get => "Ne"; }
        // End.
        // 
        // End of General section.
        // 
    }
}
