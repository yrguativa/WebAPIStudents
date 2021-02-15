pipeline {
    agent any
    
    environment {
        dotnet = 'C:\\Program Files (x86)\\dotnet\\'
    }
    triggers {
        pollSCM 'H * * * *'
    }
    stages {
        stage('Checkout') {
            steps {
                git url:'https://github.com/yrguativa/WebAPIStudents.git', 
                branch: 'master'
            }
        }
        stage('Restore packages'){
            steps{
                bat "dotnet restore StudentsAPI.Models\\StudentsAPI.Models.csproj"
                bat "dotnet restore StudentsAPI.Data\\StudentsAPI.Data.csproj"
                bat "dotnet restore StudentsAPI.Services\\StudentsAPI.Services.csproj"
                bat "dotnet restore StudentsAPI\\StudentsAPI.csproj"
            }
        }
        stage('Clean'){
            steps{
                bat "dotnet clean StudentsAPI.Models\\StudentsAPI.Models.csproj"
                bat "dotnet clean StudentsAPI.Data\\StudentsAPI.Data.csproj"
                bat "dotnet clean StudentsAPI.Services\\StudentsAPI.Services.csproj"
                bat "dotnet clean StudentsAPI\\StudentsAPI.csproj"
            }
        }
        stage('Build'){
            steps{
                bat "dotnet build  StudentsAPI.Models\\StudentsAPI.Models.csproj --configuration Release"
                bat "dotnet build  StudentsAPI.Data\\StudentsAPI.Data.csproj --configuration Release"
                bat "dotnet build  StudentsAPI.Services\\StudentsAPI.Services.csproj --configuration Release"
                bat "dotnet build  StudentsAPI\\StudentsAPI.csproj --configuration Release"
            }
        }
        stage('SonarQubeAnalysis') {
            steps {
                script {
                    def scannerHome = tool 'SonarScannerforMSBuild';
                    withSonarQubeEnv('SonarQube_8_6') {
                        bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"ApiStudents\" /d:sonar.login=\"170b7389c55b1eb2f183871ca60cac5be1d2c4d4\" "
                        bat "dotnet build  StudentsAPI.Models\\StudentsAPI.Models.csproj"
                        bat "dotnet build  StudentsAPI.Data\\StudentsAPI.Data.csproj"
                        bat "dotnet build  StudentsAPI.Services\\StudentsAPI.Services.csproj"
                        bat "dotnet build  StudentsAPI\\StudentsAPI.csproj"
                        bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end /d:sonar.login=\"170b7389c55b1eb2f183871ca60cac5be1d2c4d4\""
                    }
                }
                
            }
        }    
    }
}
