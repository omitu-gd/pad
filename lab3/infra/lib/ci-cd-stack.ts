import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as codebuild from 'aws-cdk-lib/aws-codebuild';

export class CICDStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const gitHubSource = codebuild.Source.gitHub({
      owner: 'omitu-gd',
      repo: 'pad',
    });

    new codebuild.Project(this, 'MyProject', {
      source: gitHubSource,
      buildSpec: codebuild.BuildSpec.fromObject({
        version: '0.2',
        phases: {
          build: {
            commands: [
              `cd consumer && npm install && npm run build`,
              `cd ../producer && npm install && npm run build`,
              `cd ../frontend && npm install && npm run build`,
              `cd ../infra && npm install && npm run build && npm run cdk deploy "*"`,
            ],
          },
        },
      }),
    });
  }
}
