export class BootstrapInterop{
    static hideModal(element){
        bootstrap.Modal.getInstance(element).hide();
    }
}

window.BootstrapInterop = BootstrapInterop;