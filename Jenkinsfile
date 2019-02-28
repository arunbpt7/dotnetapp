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
     
     post {
         failure {
            step {
              emailext body:'', subject: 'Failed', to: 'babuar@dss.nyc.gov'
              
       }
    }
         
         success {
            step {
              if (currentBuild.previousBuild != null && currentBuild.previousBuild.result != 'SUCCESS') {
                 mail to: 'babuar@dss.nyc.gov'
                 subject: "Pipeline Success: ${currentBuild.fullDisplayName}"
                 
              }  
            }
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
       steps{
         sh (" kubectl apply -f k8s.yaml")
       }
     }
   }
 }
   
