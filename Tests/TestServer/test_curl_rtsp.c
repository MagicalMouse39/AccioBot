#include <stdio.h>
#include <curl/curl.h>
#include <string.h>
#include <assert.h>

/* error handling macros */
#define my_curl_easy_setopt(A, B, C, action_fail) \
    if ((res = curl_easy_setopt((A), (B), (C))) != CURLE_OK){ \
        fprintf(stderr, "[rtsp error] curl_easy_setopt(%s, %s, %s) failed: %d\n", #A, #B, #C, res); \
        printf("[rtsp error] could not configure rtsp capture properly, \n\t\tplease check your parameters. \nExiting...\n\n"); \
        action_fail; \
    }

#define my_curl_easy_perform(A) \
    if ((res = curl_easy_perform((A))) != CURLE_OK){ \
        fprintf(stderr, "[rtsp error] curl_easy_perform(%s) failed: %d\n", #A, res); \
        printf("[rtsp error] could not configure rtsp capture properly, \n\t\tplease check your parameters. \nExiting...\n\n"); \
        return NULL; \
    }

int main()
{
    const char *uri = "rtsp://admin:admin@localhost:5000/cam";

    CURL *curl;
    
    /* initialize curl */
    CURLcode res = curl_global_init(CURL_GLOBAL_ALL);
    if (res != CURLE_OK) {
        fprintf(stderr, "[rtsp] curl_global_init(%s) failed: %d\n",
            "CURL_GLOBAL_ALL", res);
        return -1;
    }
    curl_version_info_data *data = curl_version_info(CURLVERSION_NOW);
    fprintf(stderr, "[rtsp]    cURL V%s loaded\n", data->version);

    /* initialize this curl session */
    curl = curl_easy_init();
    if (curl == NULL) {
        curl_global_cleanup();
        fprintf(stderr, "[rtsp] curl_easy_init() failed\n");
        return -1;
    }

    my_curl_easy_setopt(curl, CURLOPT_NOSIGNAL, 1, goto error); //This tells curl not to use any functions that install signal handlers or cause signals to be sent to your process.
    //my_curl_easy_setopt(curl, CURLOPT_ERRORBUFFER, 1);
    my_curl_easy_setopt(curl, CURLOPT_VERBOSE, 0L, goto error);
    my_curl_easy_setopt(curl, CURLOPT_NOPROGRESS, 1L, goto error);
    my_curl_easy_setopt(curl, CURLOPT_WRITEHEADER, stdout, goto error);
    my_curl_easy_setopt(curl, CURLOPT_URL, uri, goto error);

    my_curl_easy_setopt(curl, CURLOPT_RTSP_STREAM_URI, uri, goto error);
    my_curl_easy_setopt(curl, CURLOPT_RTSP_REQUEST, (long) CURL_RTSPREQ_GET_PARAMETER, goto error);
    
    char control[1500] = "";
    char user[1500] = "";
    char pass[1500] = "";
    char *strtoken;

    sscanf(uri, "rtsp://%1500s", control);
    strtoken = strtok(control, ":");
    assert(strtoken != NULL);
    strncpy(user, strtoken, sizeof user - 1);
    strtoken = strtok(NULL, "@");
    if (strtoken != NULL) {
        strncpy(pass, strtoken, sizeof pass - 1);
        printf("User: %s\nPass: %s\n", user, pass);
    }

    strtoken = strtok(NULL, "@");
    printf("%s\n\n", strtoken);

    printf("%d - %d\n", curl_easy_perform(curl), CURLE_OK);

    return 0;

error:
    printf("Error...\n");
    return -1;
}