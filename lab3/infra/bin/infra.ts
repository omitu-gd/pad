#!/usr/bin/env node
import 'source-map-support/register';
import * as cdk from 'aws-cdk-lib';
import { BackendStack } from '../lib/backend-stack';
import { FrontendStack } from '../lib/frontend-stack';
import { IAMServicePermissionsStack } from '../lib/service-permissions-stack';

const app = new cdk.App();

const stackProps = {
  env: {
    account: '656983766737',
    region: 'eu-west-1',
  },
};

new IAMServicePermissionsStack(app, 'IAMServicePermissionsStack', stackProps);
new BackendStack(app, 'Backend-Stack', stackProps);
new FrontendStack(app, 'Frontend-Stack', stackProps);
