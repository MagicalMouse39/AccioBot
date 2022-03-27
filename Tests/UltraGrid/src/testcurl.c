#include <stdio.h>
#include <curl/curl.h>

int main()
{
	printf("%s %d\n", CURLOPT_RTSP_REQUEST, CURL_RTSPREQ_OPTIONS);
	return 0;
}
