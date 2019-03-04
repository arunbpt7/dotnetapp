pipeline {
   environment {
     registry = "arunbpt7/dotnetcore"
     registryCredential = 'dockerhub'
     dockerImage = ''
   }
   agent any
   stages {
     stage('Cloning Git') {
       steps {
         git 'https://github.com/arunbpt7/dotnetapp.git'
       }
     }
     stage('Building App image') {
       steps{
         script {
           dockerImage = docker.build registry + ":latest"
            
         }
       }
     }
      
        
           
     stage('Deploy Image to docker registry ') {
       steps{
         script {
           docker.withRegistry( '', registryCredential ) {
             dockerImage.push()
           }
         }
       }
     }
     stage('Remove Unused docker image') {
       steps{
         sh "docker rmi $registry:latest"
       }
     }
     
     stage('Email Notification' ) {
        steps{
         emailext body: 'Build has been completed and application is ready to deploy on  Openshift TEST ENV ', subject: 'Jenkins | App Build and Docker Image Push | Success  ', to: 'babuar@dss.nyc.gov'
       }
     }       
 
                
      stage('APP Deployment on TEST ENV | OpenShift ') {
         input{
             message "Approve Deployment?"
         }                          
       steps{
         
         sh ("kubectl --kubeconfig /var/jenkins_home/config_openshift delete -f test_ops.yaml -n test ; kubectl --kubeconfig /var/jenkins_home/config_openshift apply -f test_ops.yaml -n test")
       }
     }
      
      stage('Deployment completion  ' ) {
        steps{
         
           
         emailext body: 'Application has been deployed  and available through "http://dontnetprod.apps.ux.hra.nycnet" ', subject: 'Jenkins | completed  | Success  ', to: 'babuar@dss.nyc.gov'
       }
     }       
   }
 }
