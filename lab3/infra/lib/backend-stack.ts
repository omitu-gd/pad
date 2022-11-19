import * as cdk from 'aws-cdk-lib';
import * as path from 'path';
import { Construct } from 'constructs';
import * as lambda from 'aws-cdk-lib/aws-lambda';
import * as sqs from 'aws-cdk-lib/aws-sqs';
import { SqsEventSource } from 'aws-cdk-lib/aws-lambda-event-sources';

export class BackendStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const queue = new sqs.Queue(this, 'MyQueue');

    const producer = new lambda.Function(this, 'Producer', {
      code: lambda.Code.fromAsset(path.resolve(__dirname, '../../producer/dist')),
      handler: 'index.handler',
      runtime: lambda.Runtime.NODEJS_16_X,
      environment: {
        QUEUE_URL: queue.queueUrl,
      },
    });

    queue.grantSendMessages(producer);

    const consumer = new lambda.Function(this, 'Consumer', {
      code: lambda.Code.fromAsset(path.resolve(__dirname, '../../consumer/dist')),
      handler: 'index.handler',
      runtime: lambda.Runtime.NODEJS_16_X,
    });

    const eventSource = new SqsEventSource(queue);
    consumer.addEventSource(eventSource);
  }
}
