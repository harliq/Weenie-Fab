using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;

namespace WeenieFab
{
    public partial class MainWindow : Window
    {
        public void CreateDataTable()
        {
            DataColumn propertyInt = new DataColumn("Property");
            DataColumn propertyInt64 = new DataColumn("Property");
            DataColumn propertyFloat = new DataColumn("Property");
            DataColumn propertyBool = new DataColumn("Property");
            DataColumn propertyString = new DataColumn("Property");
            DataColumn propertyDiD = new DataColumn("Property");

            propertyInt.DataType = Type.GetType("System.Int32");
            propertyInt64.DataType = Type.GetType("System.Int32");
            propertyFloat.DataType = Type.GetType("System.Int32");
            propertyBool.DataType = Type.GetType("System.Int32");
            propertyString.DataType = Type.GetType("System.Int32");
            propertyDiD.DataType = Type.GetType("System.Int32");

            DataColumn valueInt = new DataColumn("Value");
            DataColumn valueInt64 = new DataColumn("Value");
            DataColumn valueFloat = new DataColumn("Value");
            DataColumn valueBool = new DataColumn("Value");
            DataColumn valueString = new DataColumn("Value");
            DataColumn valueDiD = new DataColumn("Value");

            valueInt.DataType = Type.GetType("System.Int32");
            valueInt64.DataType = Type.GetType("System.Decimal");
            valueFloat.DataType = Type.GetType("System.Single");
            valueBool.DataType = Type.GetType("System.Boolean");
            valueString.DataType = Type.GetType("System.String");
            valueDiD.DataType = Type.GetType("System.String");

            DataColumn descriptionInt = new DataColumn("Description");
            DataColumn descriptionInt64 = new DataColumn("Description");
            DataColumn descriptionFloat = new DataColumn("Description");
            DataColumn descriptionBool = new DataColumn("Description");
            DataColumn descriptionString = new DataColumn("Description");
            DataColumn descriptionDiD = new DataColumn("Description");
            DataColumn descriptionSpellBook = new DataColumn("Description");

            // Int32
            integerDataTable.Columns.Add(propertyInt);
            integerDataTable.Columns.Add(valueInt);
            integerDataTable.Columns.Add(descriptionInt);
            dgInt32.DataContext = integerDataTable.DefaultView;

            // Int64
            integer64DataTable.Columns.Add(propertyInt64);
            integer64DataTable.Columns.Add(valueInt64);
            integer64DataTable.Columns.Add(descriptionInt64);
            dgInt64.DataContext = integer64DataTable.DefaultView;

            // Bool Table
            boolDataTable.Columns.Add(propertyBool);
            boolDataTable.Columns.Add(valueBool);
            boolDataTable.Columns.Add(descriptionBool);
            dgBool.DataContext = boolDataTable;

            // Float Table
            floatDataTable.Columns.Add(propertyFloat);
            floatDataTable.Columns.Add(valueFloat);
            floatDataTable.Columns.Add(descriptionFloat);
            dgFloat.DataContext = floatDataTable;

            // DiD
            didDataTable.Columns.Add(propertyDiD);
            didDataTable.Columns.Add(valueDiD);
            didDataTable.Columns.Add(descriptionDiD);
            dgDiD.DataContext = didDataTable;

            // Strings Table
            stringDataTable.Columns.Add(propertyString);
            stringDataTable.Columns.Add(valueString);
            stringDataTable.Columns.Add(descriptionString);
            dgString.DataContext = stringDataTable;

            // InstanceID Table
            DataColumn propertyIID = new DataColumn("Property");
            DataColumn valueIID = new DataColumn("Value");
            DataColumn descriptionIID = new DataColumn("Description");

            propertyIID.DataType = Type.GetType("System.Int32");
            valueIID.DataType = Type.GetType("System.Int32");

            iidDataTable.Columns.Add(propertyIID);
            iidDataTable.Columns.Add(valueIID);
            iidDataTable.Columns.Add(descriptionIID);
            cbIidProps.DataContext = iidDataTable;

            //SpellBook
            DataColumn spellIdInt = new DataColumn("Property");
            DataColumn probabilityFloat = new DataColumn("Value");
            spellIdInt.DataType = Type.GetType("System.Int32");
            probabilityFloat.DataType = Type.GetType("System.Single");

            spellDataTable.Columns.Add(spellIdInt);
            spellDataTable.Columns.Add(probabilityFloat);
            spellDataTable.Columns.Add(descriptionSpellBook);
            dgSpell.DataContext = spellDataTable;

            //Attributes
            DataColumn typeAttrib = new DataColumn("Type");
            DataColumn initLevelAttrib = new DataColumn("InitLevel");
            DataColumn levelCPAttrib = new DataColumn("LevelCP");
            DataColumn cpSpentAttrib = new DataColumn("CPSpent");
            DataColumn descriptionAttrib = new DataColumn("Description");
            typeAttrib.DataType = Type.GetType("System.Int32");
            initLevelAttrib.DataType = Type.GetType("System.Single");
            levelCPAttrib.DataType = Type.GetType("System.Single");
            cpSpentAttrib.DataType = Type.GetType("System.Single");
            attributeDataTable.Columns.Add(typeAttrib);
            attributeDataTable.Columns.Add(initLevelAttrib);
            attributeDataTable.Columns.Add(levelCPAttrib);
            attributeDataTable.Columns.Add(cpSpentAttrib);
            attributeDataTable.Columns.Add(descriptionAttrib);
            dgAttributes.DataContext = attributeDataTable;

            //Attributes 2
            DataColumn typeAttrib2 = new DataColumn("Type");
            DataColumn initLevelAttrib2 = new DataColumn("InitLevel");
            DataColumn levelCPAttrib2 = new DataColumn("LevelCP");
            DataColumn cpSpentAttrib2 = new DataColumn("CPSpent");
            DataColumn currentLevelAttrib2 = new DataColumn("CurrentLevel");
            DataColumn descriptionAttrib2 = new DataColumn("Description");
            typeAttrib2.DataType = Type.GetType("System.Int32");
            initLevelAttrib2.DataType = Type.GetType("System.Single");
            levelCPAttrib2.DataType = Type.GetType("System.Single");
            cpSpentAttrib2.DataType = Type.GetType("System.Single");
            currentLevelAttrib2.DataType = Type.GetType("System.Single");
            attribute2DataTable.Columns.Add(typeAttrib2);
            attribute2DataTable.Columns.Add(initLevelAttrib2);
            attribute2DataTable.Columns.Add(levelCPAttrib2);
            attribute2DataTable.Columns.Add(cpSpentAttrib2);
            attribute2DataTable.Columns.Add(currentLevelAttrib2);
            attribute2DataTable.Columns.Add(descriptionAttrib2);
            dgAttributesTwo.DataContext = attribute2DataTable;

            //Skills - TODO: need to fix some of the types
            DataColumn typeSkills = new DataColumn("Type");
            DataColumn levelPPSkills = new DataColumn("LevelPP");
            DataColumn sacSkills = new DataColumn("SAC");
            DataColumn ppSkills = new DataColumn("PP");
            DataColumn initLevelSkills = new DataColumn("InitLevel");
            DataColumn resistLCSkills = new DataColumn("ResistLC");
            DataColumn lastUsedSkills = new DataColumn("LastUsed");
            DataColumn descriptionSkills = new DataColumn("Description");
            typeSkills.DataType = Type.GetType("System.Int32");
            levelPPSkills.DataType = Type.GetType("System.Single");
            sacSkills.DataType = Type.GetType("System.Single");
            ppSkills.DataType = Type.GetType("System.Single");
            initLevelSkills.DataType = Type.GetType("System.Single");
            resistLCSkills.DataType = Type.GetType("System.Single");
            // lastUsedSkills.DataType = Type.GetType("System.Single");
            skillsDataTable.Columns.Add(typeSkills);
            skillsDataTable.Columns.Add(levelPPSkills);
            skillsDataTable.Columns.Add(sacSkills);
            skillsDataTable.Columns.Add(ppSkills);
            skillsDataTable.Columns.Add(initLevelSkills);
            skillsDataTable.Columns.Add(resistLCSkills);
            skillsDataTable.Columns.Add(lastUsedSkills);
            skillsDataTable.Columns.Add(descriptionSkills);
            dgSkills.DataContext = skillsDataTable;

            // Create List
            DataColumn destTypeCreateList = new DataColumn("DestinationType");
            DataColumn wcidCreateList = new DataColumn("WCID");
            DataColumn stackSizeCreateList = new DataColumn("StackSize");
            DataColumn paletteCreateList = new DataColumn("Palette");
            DataColumn shadeCreateList = new DataColumn("DropPercent");
            DataColumn tryToBondCreateList = new DataColumn("TryToBond");
            DataColumn descriptionCreateList = new DataColumn("Description");

            destTypeCreateList.DataType = Type.GetType("System.Int32");
            wcidCreateList.DataType = Type.GetType("System.Int32");
            stackSizeCreateList.DataType = Type.GetType("System.Int32");
            paletteCreateList.DataType = Type.GetType("System.Int32");
            shadeCreateList.DataType = Type.GetType("System.Single");
            tryToBondCreateList.DataType = Type.GetType("System.Boolean");

            createListDataTable.Columns.Add(destTypeCreateList);
            createListDataTable.Columns.Add(wcidCreateList);
            createListDataTable.Columns.Add(stackSizeCreateList);
            createListDataTable.Columns.Add(paletteCreateList);
            createListDataTable.Columns.Add(shadeCreateList);
            createListDataTable.Columns.Add(tryToBondCreateList);
            createListDataTable.Columns.Add(descriptionCreateList);
            dgCreateItems.DataContext = createListDataTable;

            // Body Parts
            DataColumn bodyPart = new DataColumn("BodyPart");
            DataColumn damageType = new DataColumn("DamageType");
            DataColumn damageValue = new DataColumn("Damage");
            DataColumn damageVariance = new DataColumn("DamageVariance");
            DataColumn armorLevel = new DataColumn("ArmorLevel");
            DataColumn armorLevelSlash = new DataColumn("ArmorLevelSlash");
            DataColumn armorLevelPierce = new DataColumn("ArmorLevelPierce");
            DataColumn armorLevelBludgeon = new DataColumn("ArmorLevelBludgeon");
            DataColumn armorLevelCold = new DataColumn("ArmorLevelCold");
            DataColumn armorLevelFire = new DataColumn("ArmorLevelFire");
            DataColumn armorLevelAcid = new DataColumn("ArmorLevelAcid");
            DataColumn armorLevelElectric = new DataColumn("ArmorLevelElectric");
            DataColumn armorLevelNether = new DataColumn("ArmorLevelNether");

            DataColumn baseHeight = new DataColumn("Height");

            DataColumn hlfQuad = new DataColumn("HLF");
            DataColumn mlfQuad = new DataColumn("MLF");
            DataColumn llfQuad = new DataColumn("LLF");

            DataColumn hrfQuad = new DataColumn("HRF");
            DataColumn mrfQuad = new DataColumn("MRF");
            DataColumn lrfQuad = new DataColumn("LRF");

            DataColumn hlbQuad = new DataColumn("HLB");
            DataColumn mlbQuad = new DataColumn("MLB");
            DataColumn llbQuad = new DataColumn("LLB");

            DataColumn hrbQuad = new DataColumn("HRB");
            DataColumn mrbQuad = new DataColumn("MRB");
            DataColumn lrbQuad = new DataColumn("LRB");

            DataColumn descriptionBodyParts = new DataColumn("Description");

            bodyPart.DataType = Type.GetType("System.Int32");
            damageType.DataType = Type.GetType("System.Int32");
            damageValue.DataType = Type.GetType("System.Int32");
            damageVariance.DataType = Type.GetType("System.Single");

            armorLevel.DataType = Type.GetType("System.Int32");
            armorLevelSlash.DataType = Type.GetType("System.Int32");
            armorLevelPierce.DataType = Type.GetType("System.Int32");
            armorLevelBludgeon.DataType = Type.GetType("System.Int32");
            armorLevelCold.DataType = Type.GetType("System.Int32");
            armorLevelFire.DataType = Type.GetType("System.Int32");
            armorLevelAcid.DataType = Type.GetType("System.Int32");
            armorLevelElectric.DataType = Type.GetType("System.Int32");
            armorLevelNether.DataType = Type.GetType("System.Int32");

            baseHeight.DataType = Type.GetType("System.Int32");

            hlfQuad.DataType = Type.GetType("System.Single");
            mlfQuad.DataType = Type.GetType("System.Single");
            llfQuad.DataType = Type.GetType("System.Single");

            hrfQuad.DataType = Type.GetType("System.Single");
            mrfQuad.DataType = Type.GetType("System.Single");
            lrfQuad.DataType = Type.GetType("System.Single");

            hlbQuad.DataType = Type.GetType("System.Single");
            mlbQuad.DataType = Type.GetType("System.Single");
            llbQuad.DataType = Type.GetType("System.Single");

            hrbQuad.DataType = Type.GetType("System.Single");
            mrbQuad.DataType = Type.GetType("System.Single");
            lrbQuad.DataType = Type.GetType("System.Single");

            bodypartsDataTable.Columns.Add(bodyPart);
            bodypartsDataTable.Columns.Add(damageType);
            bodypartsDataTable.Columns.Add(damageValue);
            bodypartsDataTable.Columns.Add(damageVariance);

            bodypartsDataTable.Columns.Add(armorLevel);
            bodypartsDataTable.Columns.Add(armorLevelSlash);
            bodypartsDataTable.Columns.Add(armorLevelPierce);
            bodypartsDataTable.Columns.Add(armorLevelBludgeon);
            bodypartsDataTable.Columns.Add(armorLevelCold);
            bodypartsDataTable.Columns.Add(armorLevelFire);
            bodypartsDataTable.Columns.Add(armorLevelAcid);
            bodypartsDataTable.Columns.Add(armorLevelElectric);
            bodypartsDataTable.Columns.Add(armorLevelNether);

            bodypartsDataTable.Columns.Add(baseHeight);

            bodypartsDataTable.Columns.Add(hlfQuad);
            bodypartsDataTable.Columns.Add(mlfQuad);
            bodypartsDataTable.Columns.Add(llfQuad);

            bodypartsDataTable.Columns.Add(hrfQuad);
            bodypartsDataTable.Columns.Add(mrfQuad);
            bodypartsDataTable.Columns.Add(lrfQuad);

            bodypartsDataTable.Columns.Add(hlbQuad);
            bodypartsDataTable.Columns.Add(mlbQuad);
            bodypartsDataTable.Columns.Add(llbQuad);

            bodypartsDataTable.Columns.Add(hrbQuad);
            bodypartsDataTable.Columns.Add(mrbQuad);
            bodypartsDataTable.Columns.Add(lrbQuad);

            bodypartsDataTable.Columns.Add(descriptionBodyParts);
            dgBodyParts.DataContext = bodypartsDataTable;

            // Book Properties
            DataColumn maxPages = new DataColumn("MaxPages");
            DataColumn maxCharsPage = new DataColumn("MaxCharsPage");

            maxPages.DataType = Type.GetType("System.Int32");
            maxCharsPage.DataType = Type.GetType("System.Int32");

            bookInfoDataTable.Columns.Add(maxPages);
            bookInfoDataTable.Columns.Add(maxCharsPage);

            dgBookInfo.DataContext = bookInfoDataTable;

            // Book Pages
            DataColumn pageIdBook = new DataColumn("PageID");
            DataColumn authorIdBook = new DataColumn("AuthorID");
            DataColumn authorNameBook = new DataColumn("AuthorName");
            DataColumn authorAccountBook = new DataColumn("AuthorAccount");
            DataColumn ignoreAuthorBook = new DataColumn("IgnoreAuthor");
            DataColumn pageTextBook = new DataColumn("PageText");

            pageIdBook.DataType = Type.GetType("System.Int32");
            // authorIdBook.DataType = Type.GetType("System.Single");
            ignoreAuthorBook.DataType = Type.GetType("System.Boolean");

            bookPagesDataTable.Columns.Add(pageIdBook);
            bookPagesDataTable.Columns.Add(authorIdBook);
            bookPagesDataTable.Columns.Add(authorNameBook);
            bookPagesDataTable.Columns.Add(authorAccountBook);
            bookPagesDataTable.Columns.Add(ignoreAuthorBook);
            bookPagesDataTable.Columns.Add(pageTextBook);

            dgBookPages.DataContext = bookPagesDataTable;

            // Generator


            // Positions
            DataColumn positionType = new DataColumn("PositionType");
            DataColumn cellID = new DataColumn("CellID");

            DataColumn xOrigin = new DataColumn("OriginX");
            DataColumn yOrigin = new DataColumn("OriginY");
            DataColumn zOrigin = new DataColumn("OriginZ");
            DataColumn wAngle = new DataColumn("AngleW");
            DataColumn xAngle = new DataColumn("AngleX");
            DataColumn yAngle = new DataColumn("AngleY");
            DataColumn zAngle = new DataColumn("AngleZ");

            DataColumn positionDescription = new DataColumn("Description");

            positionType.DataType = Type.GetType("System.Int32");
            cellID.DataType = Type.GetType("System.Int64");

            xOrigin.DataType = Type.GetType("System.Single");
            yOrigin.DataType = Type.GetType("System.Single");
            zOrigin.DataType = Type.GetType("System.Single");

            wAngle.DataType = Type.GetType("System.Single");
            xAngle.DataType = Type.GetType("System.Single");
            yAngle.DataType = Type.GetType("System.Single");
            zAngle.DataType = Type.GetType("System.Single");

            positionsDataTable.Columns.Add(positionType);
            positionsDataTable.Columns.Add(cellID);

            positionsDataTable.Columns.Add(xOrigin);
            positionsDataTable.Columns.Add(yOrigin);
            positionsDataTable.Columns.Add(zOrigin);

            positionsDataTable.Columns.Add(wAngle);
            positionsDataTable.Columns.Add(xAngle);
            positionsDataTable.Columns.Add(yAngle);
            positionsDataTable.Columns.Add(zAngle);

            positionsDataTable.Columns.Add(positionDescription);

            dgPosition.DataContext = positionsDataTable;

            // Events
            DataColumn eventId = new DataColumn("Event");
            eventId.DataType = Type.GetType("System.Int32");
            DataColumn descriptionEvent = new DataColumn("Description");

            eventDataTable.Columns.Add(eventId);
            eventDataTable.Columns.Add(descriptionEvent);
        }
    }
}
