import { Construct } from 'constructs';
import * as iam from 'aws-cdk-lib/aws-iam';
import * as cdk from 'aws-cdk-lib';
import { GithubActionsIdentityProvider, GithubActionsRole } from 'aws-cdk-github-oidc';
import GithubActionsPolicy from './policies/github-actions-policy.json';

export class IAMServicePermissionsStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const provider = new GithubActionsIdentityProvider(this, 'GithubProvider');
    const githubCDKRole = new GithubActionsRole(this, 'GithubCDKRole', {
      provider,
      owner: 'omitu-gd',
      repo: 'pad',
      roleName: 'github-actions-cdk',
    });
    attachPolicyToPrincipal(githubCDKRole, GithubActionsPolicy);
  }
}

export function attachPolicyToPrincipal(principal: iam.IPrincipal, policyDocument: unknown): void {
  // eslint-disable-next-line dot-notation
  for (const statement of iam.PolicyDocument.fromJson(policyDocument)['statements']) {
    principal.addToPrincipalPolicy(statement);
  }
}
