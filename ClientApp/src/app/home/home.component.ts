import '@tensorflow/tfjs-backend-cpu';
import * as tf from '@tensorflow/tfjs';

import { Component } from '@angular/core';
import { CommonService } from '../common.service';
import * as use from '@tensorflow-models/universal-sentence-encoder';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  httpLoading = false;
  userText = "";
  probability: any;
  model: any;

  constructor(
    private commonService: CommonService
  ) { }

  ngOnInit() {
    // this.loadModel();
  }

  validateTweet() {
    let data = { text: this.userText }
    this.httpLoading = true;
    this.commonService.validateTweet(data)
      .subscribe(
        (response) => {
          this.probability = response;
          this.insertLog(response);
        }
      ).add(x => {
        this.httpLoading = false;
      });
  }

  insertLog(userLog) {
    this.commonService.insertLog(userLog)
      .subscribe();
  }

  async loadModel() {
    this.model = await tf.loadLayersModel('/assets/model.json');
  }
}
