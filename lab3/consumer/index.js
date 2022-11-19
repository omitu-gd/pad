export const handler = (event) => {
  console.log(JSON.stringify(event));

  return {
    statusCode: 200,
  };
};
