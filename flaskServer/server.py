from flask import Flask, request, jsonify
import json, os, requests, pymongo
import time

client = pymongo.MongoClient("mongodb+srv://jeffrey856:cookies123@cluster0-qkfyz.mongodb.net/test?retryWrites=true&w=majority")

def create_app(config=None):

    app = Flask(__name__)

    @app.route('/')
    def home():
        return 'Hello World!'

    @app.route('/open_close', methods=['GET'])
    def openClose(): 
        args = dict(request.args)
        
        print("HERE", args)
        
        try :
            activate = args["action"]
        except Exception as e: 
            print(e)
            return jsonify({"error": "Cannot find action"}), 404

        if activate == "1":
            # open
            resp = {
                "Open": True,
                "Message": "opening object"
            }
            with open("state.json", "w") as file:
                json.dump(resp, file)
        elif activate == "0":
        # close
            resp ={
                "Open": False,
                "Message": "closing object"
            }
            with open("state.json", "w") as file:
                json.dump(resp, file)
        else:
            try:
                f = open("state.json", "r")
                # Do something with the file
                resp = json.load(f)
            except IOError:
                print("No previous state")
                return jsonify("err: " "Cannot find action"), 404

        return resp, 200

    # @app.route('/stat', methods=['GET'])
    
    # def ledOpen(): 
        
    #     try:
    #         state = open("state.json", "r")
    #         # Do something with the file
    #         state_resp = json.load(state)
    #         print(state_resp)
    #     except IOError:
    #         print("No previous state")
    #         return jsonify("err: " "Cannot find action"), 404
        
    #         # time_resp = json.load(f)
    #         # print(time_resp)
    #     if state_resp["Open"] is True:
    #         with open("stat.json", "w") as f:
    #             print("F IS HERE ", json.load(f))
                
                                
    #         # json.dump(new_resp,f)
      

    #     return state_resp, 200   


    # @app.route('/stat', methods=['GET'])
    
    # def ledOpen(): 
        
    #     try:
    #         state = open("state.json", "r")
    #         # Do something with the file
    #         state_resp = json.load(state)
    #         print(state_resp)
    #     except IOError:
    #         print("No previous state")
    #         return jsonify("err: " "Cannot find action"), 404
        
    #         # time_resp = json.load(f)
    #         # print(time_resp)
    #     if state_resp["Open"] is True:
    #         with open("stat.json", "r") as stat:
    #             stat_state = json.load(stat)
    #             numb_times = stat_state["total_time"] + 1
    #             with open("stat.json", "w") as over:
    #                 new_state = {"time_open": numb_times, "total_time": 0}
    #                 json.dump(new_state, over)
    #             print(stat_state)

    #         return new_state, 200   
        
    #     return state_resp, 200
    
    @app.route('/query', methods=['GET'])
    def handle_exact_query():
        args = dict(request.args)
        try:
            projection = {
                'user': str(args['name'])
            }
            data = queryMongo(projection)
        except Exception as e:
            print(e)
            return jsonify({"error": 'Error in retrieving data from database.'}), 404
        try:
            res = generate_response(args, data)
        except Exception as e:
            print(e)
            return jsonify({"error": "Error in generating response for client."}), 404
        return res, 200

    def generate_response(args, data):
        tmp = [(str(d), str(data[d])) for d in data]
        print(tmp)
        ret = {
            "data": tmp,
            "parameters": args
        }
        return jsonify({"error": "Cannot find action"}), 404
    

    def queryMongo(projection):
        db = client.UserData
        col = db.User
        cur = col.find(dict(projection))
        print(cur)
        return cur[0]
        if (cur.count()):
            return cur[0]
        else:
            raise LookupError('Cannot retrieve query:', projection)

    return app


if __name__ == '__main__':
    app = create_app()
    app.run(debug=True, host='0.0.0.0', port=8080)

 