﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace LoanBroker.RuleBase {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="RuleBaseSoap", Namespace="http://localhost/")]
    public partial class RuleBase : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetBanksOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAllBankInformationForTestingOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public RuleBase() {
            this.Url = global::LoanBroker.Properties.Settings.Default.LoanBroker_RuleBase_RuleBase;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetBanksCompletedEventHandler GetBanksCompleted;
        
        /// <remarks/>
        public event GetAllBankInformationForTestingCompletedEventHandler GetAllBankInformationForTestingCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/GetBanks", RequestNamespace="http://localhost/", ResponseNamespace="http://localhost/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Bank[] GetBanks(int creditscore, bool isInRKI, double amount, System.DateTime loanDuration) {
            object[] results = this.Invoke("GetBanks", new object[] {
                        creditscore,
                        isInRKI,
                        amount,
                        loanDuration});
            return ((Bank[])(results[0]));
        }
        
        /// <remarks/>
        public void GetBanksAsync(int creditscore, bool isInRKI, double amount, System.DateTime loanDuration) {
            this.GetBanksAsync(creditscore, isInRKI, amount, loanDuration, null);
        }
        
        /// <remarks/>
        public void GetBanksAsync(int creditscore, bool isInRKI, double amount, System.DateTime loanDuration, object userState) {
            if ((this.GetBanksOperationCompleted == null)) {
                this.GetBanksOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBanksOperationCompleted);
            }
            this.InvokeAsync("GetBanks", new object[] {
                        creditscore,
                        isInRKI,
                        amount,
                        loanDuration}, this.GetBanksOperationCompleted, userState);
        }
        
        private void OnGetBanksOperationCompleted(object arg) {
            if ((this.GetBanksCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetBanksCompleted(this, new GetBanksCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/GetAllBankInformationForTesting", RequestNamespace="http://localhost/", ResponseNamespace="http://localhost/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Bank[] GetAllBankInformationForTesting() {
            object[] results = this.Invoke("GetAllBankInformationForTesting", new object[0]);
            return ((Bank[])(results[0]));
        }
        
        /// <remarks/>
        public void GetAllBankInformationForTestingAsync() {
            this.GetAllBankInformationForTestingAsync(null);
        }
        
        /// <remarks/>
        public void GetAllBankInformationForTestingAsync(object userState) {
            if ((this.GetAllBankInformationForTestingOperationCompleted == null)) {
                this.GetAllBankInformationForTestingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllBankInformationForTestingOperationCompleted);
            }
            this.InvokeAsync("GetAllBankInformationForTesting", new object[0], this.GetAllBankInformationForTestingOperationCompleted, userState);
        }
        
        private void OnGetAllBankInformationForTestingOperationCompleted(object arg) {
            if ((this.GetAllBankInformationForTestingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllBankInformationForTestingCompleted(this, new GetAllBankInformationForTestingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/")]
    public partial class Bank {
        
        private string nameField;
        
        private int minimumCreditscoreField;
        
        private int maximumCreditscoreField;
        
        private double maximumAmountField;
        
        private System.DateTime maximumDurationField;
        
        private bool allowsRKIField;
        
        private string hostField;
        
        private bool usesMessagingField;
        
        private string exchangeField;
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public int MinimumCreditscore {
            get {
                return this.minimumCreditscoreField;
            }
            set {
                this.minimumCreditscoreField = value;
            }
        }
        
        /// <remarks/>
        public int MaximumCreditscore {
            get {
                return this.maximumCreditscoreField;
            }
            set {
                this.maximumCreditscoreField = value;
            }
        }
        
        /// <remarks/>
        public double MaximumAmount {
            get {
                return this.maximumAmountField;
            }
            set {
                this.maximumAmountField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime MaximumDuration {
            get {
                return this.maximumDurationField;
            }
            set {
                this.maximumDurationField = value;
            }
        }
        
        /// <remarks/>
        public bool AllowsRKI {
            get {
                return this.allowsRKIField;
            }
            set {
                this.allowsRKIField = value;
            }
        }
        
        /// <remarks/>
        public string Host {
            get {
                return this.hostField;
            }
            set {
                this.hostField = value;
            }
        }
        
        /// <remarks/>
        public bool UsesMessaging {
            get {
                return this.usesMessagingField;
            }
            set {
                this.usesMessagingField = value;
            }
        }
        
        /// <remarks/>
        public string Exchange {
            get {
                return this.exchangeField;
            }
            set {
                this.exchangeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GetBanksCompletedEventHandler(object sender, GetBanksCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetBanksCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetBanksCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Bank[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Bank[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GetAllBankInformationForTestingCompletedEventHandler(object sender, GetAllBankInformationForTestingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllBankInformationForTestingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllBankInformationForTestingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Bank[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Bank[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591