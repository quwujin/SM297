using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;

namespace Model
{
    [XmlRoot("ReturnValue")]
    [Serializable]
    public class ReturnValue
    {
        #region Base Property

        private bool _success;

        /// <summary>
        ///	Check if method success.
        /// </summary>
        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
            }
        }

        private int _identityId;

        /// <summary>
        ///	The identity id from database.
        /// </summary>
        public int IdentityId
        {
            set
            {
                _identityId = value;
            }
            get
            {
                return _identityId;
            }
        }

        private string _errMessage;

        /// <summary>
        ///	The error message.
        /// </summary>
        public string ErrMessage
        {
            set
            {
                _errMessage = value;
            }
            get
            {
                return _errMessage;
            }
        }


        private string _sQLText;

        /// <summary>
        ///	The error message.
        /// </summary>
        public string SQLText
        {
            set
            {
                _sQLText = value;
            }
            get
            {
                return _sQLText;
            }
        }

        private int _effectRows;
        public int EffectRows
        {
            set
            {
                _effectRows = value;
            }
            get
            {
                return _effectRows;
            }
        }

        private int _code;
        public int Code
        {
            set
            {
                _code = value;
            }
            get
            {
                return _code;
            }
        }

        private string _notes;
        public string Notes
        {
            set
            {
                _notes = value;
            }
            get
            {
                return _notes;
            }
        }

        private object _objectValue;
        public object ObjectValue
        {
            set
            {
                _objectValue = value;
            }
            get
            {
                return _objectValue;
            }
        }

        private System.Collections.Generic.List<string> _messageList;
        public System.Collections.Generic.List<string> MessageList
        {
            set
            {
                _messageList = value;
            }
            get
            {
                return _messageList;
            }
        }



        #endregion

        /// <summary>
        ///	Constructor method.
        /// </summary>

        public ReturnValue()
        {
            this.Success = false;
            this.ErrMessage = "活动火爆，请稍后再来吧";
            this.MessageList = new System.Collections.Generic.List<string>();

        }
    }

}
 
