using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACBEO_TrainingsTool_NEW_WPF
{
    class SwissQRCodeStringAssembler
    {
        private string _QRType = "SPC";     //Fixed value "SPC" (Swiss Payments Code)
        private string _Version = "0200";   //
        private string _Coding_Type = 1;    // Character set code.Fixed value 1 (indicates UTF-8 restricted to the Latin character set)

        private string _IBAN = "CH07 9999 9999 9999 9999 9";  //default
        private string _AdressType = "S";       //"S" - structured address; "K" - combined address elements(2 lines)
        private string _Name = "Some Club";    //Name

        private string _StrtNmOrAdrLine1 = "";  //empty because "Vereinsadresse" without street
        private string _BldgNbOrAdrLine2 = "";  //empty because "Vereinsadresse" without street
        private string _PstCd = "3000";  //Postale code
        private string _TwnNm = "Bern";  //Town
        private string _Ctry = "CH";  //country

        private string _Amt = "0";  //Amount  0.01 and CHF/EUR 999999999.99 only . as separator, no leading 0's
        private string _Ccy = "CHF";  //Currency
        private string _ = "";  //
        private string _ = "";  //
        private string _ = "";  //
        private string _ = "";  //
        private string _ = "";  //




        



    }
}
