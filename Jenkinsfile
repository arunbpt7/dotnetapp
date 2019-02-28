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
     stage('Building image') {
       steps{
         script {
           dockerImage = docker.build registry + ":latest"
            
         }
       }
     }
          
         
           
     stage('Deploy Image') {
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
     
     stage('Send email') {
        steps{
         emailext body: 'Build and Docker image Push has been completed sucecsfully ', subject: 'Jenkins |  App Build and Docker Image Push |Success  ', to: 'babuar@dss.nyc.gov'
       }
     }       
 
   
      stage('Deploy the docker image in kubernetes') {
         input {
              message:'Approve Deployment?'
         }
       steps{
         sh (" kubectl apply -f k8s.yaml")
       }
     }
   }
 }
   
