namespace RiskApp.Controllers {

    export class HomeController {
        public message = 'Hello from the home page!';

    }


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }


    export class AboutController {
        public message = 'Hello from the about page!';

    }

    export class RiskManagementController {
        public message = 'Hello from the risk management page!';
        public orgResource;
        public organizations;
        public currentUserName;
        public selectedOrg;

        public getCurrentUser() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }

        public getOrganizations() {
            this.organizations = this.orgResource.query({ userName: this.currentUserName })
            console.log(this.organizations);
        }

        public getSelectedOrg() {
            console.log(this.selectedOrg.orgName);
        }

        public showAddOrgModal() {
            this.$uibModal.open({
                templateUrl: '/ngApp/views/addOrg.html',
                controller: 'AddOrgDialogController',
                controllerAs: 'modal',
                size: 'sm'
            });
        }

        constructor(public $state: ng.ui.IStateService, private accountService: RiskApp.Services.AccountService, private $resource: angular.resource.IResourceService, private $uibModal: angular.ui.bootstrap.IModalService) {
            this.orgResource = $resource('/api/organizations/:userName')
            this.getCurrentUser();
            this.getOrganizations();
        }
    }

    export class AddOrgDialogController {
        public orgResource;
        public newOrganization;
        public currentUserName;


        public getCurrentUser() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }

 
        public addOrganization() {
            console.log(this.newOrganization);
            this.orgResource.save({ userName: this.currentUserName }, this.newOrganization).$promise;
            this.$uibModalInstance.close();
        }
        
        public cancel() {
            this.$uibModalInstance.close();
        }

        constructor(private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, private $resource: angular.resource.IResourceService, private accountService: RiskApp.Services.AccountService) {
            this.orgResource = $resource('/api/organizations/:userName');
            this.getCurrentUser();
            }
    }

    angular.module('RiskApp').controller('AddOrgDialogController', AddOrgDialogController);

    export class UpdateOrgDialogController {
        public orgResource;
        public organization;
        public currentUserName;


        public getCurrentUser() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }


        public updateOrganization() {
            console.log(this.organization);
            this.orgResource.save({ userName: this.currentUserName }, this.organization).$promise;
            this.$uibModalInstance.close();
        }

        public cancel() {
            this.$uibModalInstance.close();
        }

        constructor(private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, private $resource: angular.resource.IResourceService, private accountService: RiskApp.Services.AccountService) {
            this.orgResource = $resource('/api/organizations/:userName');
            this.getCurrentUser();
        }
    }

    angular.module('RiskApp').controller('UpdateOrgDialogController', UpdateOrgDialogController);
}
