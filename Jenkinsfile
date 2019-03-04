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
         emailext body: 'Build and Docker Push has been completed sucecsfully and application is ready to deploy on TEST ENV ', subject: 'Jenkins | App Build and Docker Image Push | Success  ', to: 'babuar@dss.nyc.gov'
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
         
           sh ("kubectl --kubeconfig /var/jenkins_home/config_openshift get ingress dontnetprod-ingress  -n test | awk '{print $2}' >  hosts ;cat hosts
         emailext body: 'Application deployment has been compleled ', subject: 'Jenkins | Deploymemnt has completed  | Success  ', to: 'babuar@dss.nyc.gov'
       }
     }       
   }
 }
