import { Component, OnInit } from '@angular/core';
import { CommonService } from '../common.service';

@Component({
  selector: 'app-tweet-downloader',
  templateUrl: './tweet-downloader.component.html',
  styleUrls: ['./tweet-downloader.component.css']
})
export class TweetDownloaderComponent implements OnInit {
  tweetIds: number;
  iteration = 10;
  httpLoading = false;

  constructor(
    private commonService: CommonService
  ) { }

  ngOnInit() {

  }

  getMultipleTweets() {
    this.commonService.getMultipleTweets(this.tweetIds)
      .subscribe(
        (response) => {
          console.log(response)
        }
      );
  }

  downloadTweets() {
    while (this.iteration > 0 && this.httpLoading) {
      // this.iteration - 1;
      this.httpLoading = true;

      console.log("fired", this.iteration)

      this.commonService.downloadTweets()
        .subscribe(
          (response) => {
            console.log(response)
            this.iteration--;
            this.httpLoading = false;
          }
        );
    }
  }
}
