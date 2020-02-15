from flask import Flask, request, jsonify
import json, os, requests

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

    def generate_response(args, data):
        tmp = [(str(d), str(data[d])) for d in data]
        print(tmp)
        ret = {
            "data": tmp,
            "parameters": args
        }
        return jsonify({"error": "Cannot find action"}), 404
    
    return app

if __name__ == '__main__':
    app = create_app()
    app.run(debug=True, host='0.0.0.0', port=8080)

 