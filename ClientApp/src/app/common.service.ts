import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private apiUrl = environment.apiUrl;

  constructor(private HttpClient: HttpClient) { }

  getWeather() {
    return this.HttpClient.get(this.apiUrl + 'api/weatherforecast/');
  }

  getMultipleTweets(tweetId: number) {
    return this.HttpClient.get(this.apiUrl + 'api/twitter/GetMultipleTweets?tweetIds=' + tweetId);
  }

  downloadTweets() {
    return this.HttpClient.get(this.apiUrl + 'api/twitter/DownloadTweets');
  }

  validateTweet(text) {
    return this.HttpClient.post(this.apiUrl + 'api/main/validate/', text);
  }

  insertLog(userLog) {
    return this.HttpClient.post(this.apiUrl + 'api/twitter/InsertLog', userLog);
  }
}
