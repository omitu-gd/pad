#!/usr/bin/env node
import 'source-map-support/register';
import * as cdk from 'aws-cdk-lib';
import { BackendStack } from '../lib/backend-stack';
import { FrontendStack } from '../lib/frontend-stack';
import { CICDStack } from '../lib/ci-cd-stack';

const app = new cdk.App();

const stackProps = {
  env: {
    account: '656983766737',
    region: 'eu-west-1',
  },
};

new BackendStack(app, 'Backend-Stack', stackProps);
new FrontendStack(app, 'Frontend-Stack', stackProps);
new CICDStack(app, 'CICD-Stack', stackProps);
