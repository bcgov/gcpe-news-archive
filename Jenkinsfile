node('master') {
	
    stage('Build Image') {
        echo "Building..."
        openshiftBuild bldCfg: 'news-archive', showBuildLogs: 'true'
        openshiftTag destStream: 'news-archive', verbose: 'true', destTag: '$BUILD_ID', srcStream: 'news-archive', srcTag: 'latest'
    }

	stage('Deploy to Dev') {
        echo "Deploying to uat..."
		openshiftTag destStream: 'news-archive', verbose: 'true', destTag: 'dev', srcStream: 'news-archive', srcTag: '$BUILD_ID'
    }	
}

stage('Deploy on Test') {
    input "Deploy to Test?"
    node('master') {
        openshiftTag destStream: 'news-archive', verbose: 'true', destTag: 'test', srcStream: 'news-archive', srcTag: '$BUILD_ID'
    }
}

stage('Deploy on Prod') {
    input "Deploy to Prod?"
    node('master') {
        openshiftTag destStream: 'news-archive', verbose: 'true', destTag: 'prod', srcStream: 'news-archive', srcTag: '$BUILD_ID'
    }
}

