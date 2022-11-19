import { SQSClient, SendMessageCommand } from '@aws-sdk/client-sqs';

const client = new SQSClient();

export const handler = (event) => {
  const command = new SendMessageCommand({
    
    QueueUrl: process.env.QUEUE_URL,
    MessageBody: JSON.stringify(event),
  });

  return client.send(command);
};
