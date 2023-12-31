﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gyermekvasút.Hálózat {
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAllomasChannel : Gyermekvasút.Hálózat.IAllomas, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AllomasClient : System.ServiceModel.ClientBase<Gyermekvasút.Hálózat.IAllomas>, Gyermekvasút.Hálózat.IAllomas {
        
        private BeginOperationDelegate onBeginVonatElkuldDelegate;
        
        private EndOperationDelegate onEndVonatElkuldDelegate;
        
        private System.Threading.SendOrPostCallback onVonatElkuldCompletedDelegate;
        
        private BeginOperationDelegate onBeginVonatFogadDelegate;
        
        private EndOperationDelegate onEndVonatFogadDelegate;
        
        private System.Threading.SendOrPostCallback onVonatFogadCompletedDelegate;
        
        private BeginOperationDelegate onBeginOnMegcsengettekDelegate;
        
        private EndOperationDelegate onEndOnMegcsengettekDelegate;
        
        private System.Threading.SendOrPostCallback onOnMegcsengettekCompletedDelegate;
        
        private BeginOperationDelegate onBeginOnVisszacsengettekDelegate;
        
        private EndOperationDelegate onEndOnVisszacsengettekDelegate;
        
        private System.Threading.SendOrPostCallback onOnVisszacsengettekCompletedDelegate;
        
        private BeginOperationDelegate onBeginOnKozlemenyDelegate;
        
        private EndOperationDelegate onEndOnKozlemenyDelegate;
        
        private System.Threading.SendOrPostCallback onOnKozlemenyCompletedDelegate;
        
        private BeginOperationDelegate onBeginOnHivasMegszakitvaDelegate;
        
        private EndOperationDelegate onEndOnHivasMegszakitvaDelegate;
        
        private System.Threading.SendOrPostCallback onOnHivasMegszakitvaCompletedDelegate;
        
        private BeginOperationDelegate onBeginPingDelegate;
        
        private EndOperationDelegate onEndPingDelegate;
        
        private System.Threading.SendOrPostCallback onPingCompletedDelegate;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> VonatElkuldCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> VonatFogadCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OnMegcsengettekCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OnVisszacsengettekCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OnKozlemenyCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OnHivasMegszakitvaCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> PingCompleted;

        public AllomasClient()
        {
        }

        public AllomasClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public AllomasClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public AllomasClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public AllomasClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public void VonatElkuld(Gyermekvasút.Vonat vonat)
        {
            //base.Channel.VonatElkuld(vonat);
            this.BeginVonatElkuld(vonat, null, null);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginVonatElkuld(Gyermekvasút.Vonat vonat, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginVonatElkuld(vonat, callback, asyncState);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndVonatElkuld(System.IAsyncResult result)
        {
            base.Channel.EndVonatElkuld(result);
        }

        private System.IAsyncResult OnBeginVonatElkuld(object[] inValues, System.AsyncCallback callback, object asyncState)
        {
            Gyermekvasút.Vonat vonat = ((Gyermekvasút.Vonat)(inValues[0]));
            return this.BeginVonatElkuld(vonat, callback, asyncState);
        }

        private object[] OnEndVonatElkuld(System.IAsyncResult result)
        {
            this.EndVonatElkuld(result);
            return null;
        }

        private void OnVonatElkuldCompleted(object state)
        {
            if ((this.VonatElkuldCompleted != null))
            {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.VonatElkuldCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }

        public void VonatElkuldAsync(Gyermekvasút.Vonat vonat)
        {
            this.VonatElkuldAsync(vonat, null);
        }

        public void VonatElkuldAsync(Gyermekvasút.Vonat vonat, object userState)
        {
            if ((this.onBeginVonatElkuldDelegate == null))
            {
                this.onBeginVonatElkuldDelegate = new BeginOperationDelegate(this.OnBeginVonatElkuld);
            }
            if ((this.onEndVonatElkuldDelegate == null))
            {
                this.onEndVonatElkuldDelegate = new EndOperationDelegate(this.OnEndVonatElkuld);
            }
            if ((this.onVonatElkuldCompletedDelegate == null))
            {
                this.onVonatElkuldCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnVonatElkuldCompleted);
            }
            base.InvokeAsync(this.onBeginVonatElkuldDelegate, new object[] {
                        vonat}, this.onEndVonatElkuldDelegate, this.onVonatElkuldCompletedDelegate, userState);
        }

        public void VonatFogad(Gyermekvasút.Vonat vonat)
        {
            //base.Channel.VonatFogad(vonat);
            this.BeginVonatFogad(vonat, null, null);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginVonatFogad(Gyermekvasút.Vonat vonat, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginVonatFogad(vonat, callback, asyncState);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndVonatFogad(System.IAsyncResult result)
        {
            base.Channel.EndVonatFogad(result);
        }

        private System.IAsyncResult OnBeginVonatFogad(object[] inValues, System.AsyncCallback callback, object asyncState)
        {
            Gyermekvasút.Vonat vonat = ((Gyermekvasút.Vonat)(inValues[0]));
            return this.BeginVonatFogad(vonat, callback, asyncState);
        }

        private object[] OnEndVonatFogad(System.IAsyncResult result)
        {
            this.EndVonatFogad(result);
            return null;
        }

        private void OnVonatFogadCompleted(object state)
        {
            if ((this.VonatFogadCompleted != null))
            {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.VonatFogadCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }

        public void VonatFogadAsync(Gyermekvasút.Vonat vonat)
        {
            this.VonatFogadAsync(vonat, null);
        }

        public void VonatFogadAsync(Gyermekvasút.Vonat vonat, object userState)
        {
            if ((this.onBeginVonatFogadDelegate == null))
            {
                this.onBeginVonatFogadDelegate = new BeginOperationDelegate(this.OnBeginVonatFogad);
            }
            if ((this.onEndVonatFogadDelegate == null))
            {
                this.onEndVonatFogadDelegate = new EndOperationDelegate(this.OnEndVonatFogad);
            }
            if ((this.onVonatFogadCompletedDelegate == null))
            {
                this.onVonatFogadCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnVonatFogadCompleted);
            }
            base.InvokeAsync(this.onBeginVonatFogadDelegate, new object[] {
                        vonat}, this.onEndVonatFogadDelegate, this.onVonatFogadCompletedDelegate, userState);
        }
        
        public void OnMegcsengettek(bool kpFeleHiv) {
            //base.Channel.OnMegcsengettek(kpFeleHiv);
            this.BeginOnMegcsengettek(kpFeleHiv, null, null);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOnMegcsengettek(bool kpFeleHiv, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOnMegcsengettek(kpFeleHiv, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndOnMegcsengettek(System.IAsyncResult result) {
            base.Channel.EndOnMegcsengettek(result);
        }
        
        private System.IAsyncResult OnBeginOnMegcsengettek(object[] inValues, System.AsyncCallback callback, object asyncState) {
            bool kpFeleHiv = ((bool)(inValues[0]));
            return this.BeginOnMegcsengettek(kpFeleHiv, callback, asyncState);
        }
        
        private object[] OnEndOnMegcsengettek(System.IAsyncResult result) {
            this.EndOnMegcsengettek(result);
            return null;
        }
        
        private void OnOnMegcsengettekCompleted(object state) {
            if ((this.OnMegcsengettekCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OnMegcsengettekCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OnMegcsengettekAsync(bool kpFeleHiv) {
            this.OnMegcsengettekAsync(kpFeleHiv, null);
        }
        
        public void OnMegcsengettekAsync(bool kpFeleHiv, object userState) {
            if ((this.onBeginOnMegcsengettekDelegate == null)) {
                this.onBeginOnMegcsengettekDelegate = new BeginOperationDelegate(this.OnBeginOnMegcsengettek);
            }
            if ((this.onEndOnMegcsengettekDelegate == null)) {
                this.onEndOnMegcsengettekDelegate = new EndOperationDelegate(this.OnEndOnMegcsengettek);
            }
            if ((this.onOnMegcsengettekCompletedDelegate == null)) {
                this.onOnMegcsengettekCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOnMegcsengettekCompleted);
            }
            base.InvokeAsync(this.onBeginOnMegcsengettekDelegate, new object[] {
                        kpFeleHiv}, this.onEndOnMegcsengettekDelegate, this.onOnMegcsengettekCompletedDelegate, userState);
        }
        
        public void OnVisszacsengettek(bool kpFeleHiv) {
            //base.Channel.OnVisszacsengettek(kpFeleHiv);
            this.BeginOnVisszacsengettek(kpFeleHiv, null, null);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOnVisszacsengettek(bool kpFeleHiv, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOnVisszacsengettek(kpFeleHiv, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndOnVisszacsengettek(System.IAsyncResult result) {
            base.Channel.EndOnVisszacsengettek(result);
        }
        
        private System.IAsyncResult OnBeginOnVisszacsengettek(object[] inValues, System.AsyncCallback callback, object asyncState) {
            bool kpFeleHiv = ((bool)(inValues[0]));
            return this.BeginOnVisszacsengettek(kpFeleHiv, callback, asyncState);
        }
        
        private object[] OnEndOnVisszacsengettek(System.IAsyncResult result) {
            this.EndOnVisszacsengettek(result);
            return null;
        }
        
        private void OnOnVisszacsengettekCompleted(object state) {
            if ((this.OnVisszacsengettekCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OnVisszacsengettekCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OnVisszacsengettekAsync(bool kpFeleHiv) {
            this.OnVisszacsengettekAsync(kpFeleHiv, null);
        }
        
        public void OnVisszacsengettekAsync(bool kpFeleHiv, object userState) {
            if ((this.onBeginOnVisszacsengettekDelegate == null)) {
                this.onBeginOnVisszacsengettekDelegate = new BeginOperationDelegate(this.OnBeginOnVisszacsengettek);
            }
            if ((this.onEndOnVisszacsengettekDelegate == null)) {
                this.onEndOnVisszacsengettekDelegate = new EndOperationDelegate(this.OnEndOnVisszacsengettek);
            }
            if ((this.onOnVisszacsengettekCompletedDelegate == null)) {
                this.onOnVisszacsengettekCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOnVisszacsengettekCompleted);
            }
            base.InvokeAsync(this.onBeginOnVisszacsengettekDelegate, new object[] {
                        kpFeleHiv}, this.onEndOnVisszacsengettekDelegate, this.onOnVisszacsengettekCompletedDelegate, userState);
        }
        
        public void OnKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters) {
            //base.Channel.OnKozlemeny(kpFeleHiv, kozlemenyTipus, parameters);
            this.BeginOnKozlemeny(kpFeleHiv, kozlemenyTipus, parameters, null, null);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOnKozlemeny(bool kpFeleHiv, int kozlemenyTipus, object[] parameters, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOnKozlemeny(kpFeleHiv, kozlemenyTipus, parameters, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndOnKozlemeny(System.IAsyncResult result) {
            base.Channel.EndOnKozlemeny(result);
        }
        
        private System.IAsyncResult OnBeginOnKozlemeny(object[] inValues, System.AsyncCallback callback, object asyncState) {
            bool kpFeleHiv = ((bool)(inValues[0]));
            int kozlemenyTipus = ((int)(inValues[1]));
            object[] parameters = ((object[])(inValues[2]));
            return this.BeginOnKozlemeny(kpFeleHiv, kozlemenyTipus, parameters, callback, asyncState);
        }
        
        private object[] OnEndOnKozlemeny(System.IAsyncResult result) {
            this.EndOnKozlemeny(result);
            return null;
        }
        
        private void OnOnKozlemenyCompleted(object state) {
            if ((this.OnKozlemenyCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OnKozlemenyCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OnKozlemenyAsync(bool kpFeleHiv, int kozlemenyTipus, object[] parameters) {
            this.OnKozlemenyAsync(kpFeleHiv, kozlemenyTipus, parameters, null);
        }
        
        public void OnKozlemenyAsync(bool kpFeleHiv, int kozlemenyTipus, object[] parameters, object userState) {
            if ((this.onBeginOnKozlemenyDelegate == null)) {
                this.onBeginOnKozlemenyDelegate = new BeginOperationDelegate(this.OnBeginOnKozlemeny);
            }
            if ((this.onEndOnKozlemenyDelegate == null)) {
                this.onEndOnKozlemenyDelegate = new EndOperationDelegate(this.OnEndOnKozlemeny);
            }
            if ((this.onOnKozlemenyCompletedDelegate == null)) {
                this.onOnKozlemenyCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOnKozlemenyCompleted);
            }
            base.InvokeAsync(this.onBeginOnKozlemenyDelegate, new object[] {
                        kpFeleHiv,
                        kozlemenyTipus,
                        parameters}, this.onEndOnKozlemenyDelegate, this.onOnKozlemenyCompletedDelegate, userState);
        }
        
        public void OnHivasMegszakitva(bool kpFeleHiv) {
            //base.Channel.OnHivasMegszakitva(kpFeleHiv);
            this.BeginOnHivasMegszakitva(kpFeleHiv, null, null);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginOnHivasMegszakitva(bool kpFeleHiv, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginOnHivasMegszakitva(kpFeleHiv, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndOnHivasMegszakitva(System.IAsyncResult result) {
            base.Channel.EndOnHivasMegszakitva(result);
        }
        
        private System.IAsyncResult OnBeginOnHivasMegszakitva(object[] inValues, System.AsyncCallback callback, object asyncState) {
            bool kpFeleHiv = ((bool)(inValues[0]));
            return this.BeginOnHivasMegszakitva(kpFeleHiv, callback, asyncState);
        }
        
        private object[] OnEndOnHivasMegszakitva(System.IAsyncResult result) {
            this.EndOnHivasMegszakitva(result);
            return null;
        }
        
        private void OnOnHivasMegszakitvaCompleted(object state) {
            if ((this.OnHivasMegszakitvaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OnHivasMegszakitvaCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OnHivasMegszakitvaAsync(bool kpFeleHiv) {
            this.OnHivasMegszakitvaAsync(kpFeleHiv, null);
        }
        
        public void OnHivasMegszakitvaAsync(bool kpFeleHiv, object userState) {
            if ((this.onBeginOnHivasMegszakitvaDelegate == null)) {
                this.onBeginOnHivasMegszakitvaDelegate = new BeginOperationDelegate(this.OnBeginOnHivasMegszakitva);
            }
            if ((this.onEndOnHivasMegszakitvaDelegate == null)) {
                this.onEndOnHivasMegszakitvaDelegate = new EndOperationDelegate(this.OnEndOnHivasMegszakitva);
            }
            if ((this.onOnHivasMegszakitvaCompletedDelegate == null)) {
                this.onOnHivasMegszakitvaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOnHivasMegszakitvaCompleted);
            }
            base.InvokeAsync(this.onBeginOnHivasMegszakitvaDelegate, new object[] {
                        kpFeleHiv}, this.onEndOnHivasMegszakitvaDelegate, this.onOnHivasMegszakitvaCompletedDelegate, userState);
        }
        
        public void Ping(bool kpFelePingel) {
            //base.Channel.Ping(kpFelePingel);
            this.BeginPing(kpFelePingel, null, null);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginPing(bool kpFelePingel, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginPing(kpFelePingel, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndPing(System.IAsyncResult result) {
            base.Channel.EndPing(result);
        }
        
        private System.IAsyncResult OnBeginPing(object[] inValues, System.AsyncCallback callback, object asyncState) {
            bool kpFelePingel = ((bool)(inValues[0]));
            return this.BeginPing(kpFelePingel, callback, asyncState);
        }
        
        private object[] OnEndPing(System.IAsyncResult result) {
            this.EndPing(result);
            return null;
        }
        
        private void OnPingCompleted(object state) {
            if ((this.PingCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.PingCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void PingAsync(bool kpFelePingel) {
            this.PingAsync(kpFelePingel, null);
        }
        
        public void PingAsync(bool kpFelePingel, object userState) {
            if ((this.onBeginPingDelegate == null)) {
                this.onBeginPingDelegate = new BeginOperationDelegate(this.OnBeginPing);
            }
            if ((this.onEndPingDelegate == null)) {
                this.onEndPingDelegate = new EndOperationDelegate(this.OnEndPing);
            }
            if ((this.onPingCompletedDelegate == null)) {
                this.onPingCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnPingCompleted);
            }
            base.InvokeAsync(this.onBeginPingDelegate, new object[] {
                        kpFelePingel}, this.onEndPingDelegate, this.onPingCompletedDelegate, userState);
        }


        public event TelCsengetesDelegate Megcsengettek;

        public event TelCsengetesDelegate Visszacsengettek;

        public event TelKozlemenyDelegate Kozlemeny;

        public event TelMegszakitvaDelegate HivasMegszakitva;
    }
}
